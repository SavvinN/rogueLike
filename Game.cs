using rogueLike.GameObjects;
using rogueLike.GameObjects.Enemys;
using rogueLike.GameObjects.Items;
using rogueLike.GameObjects.MazeObjects;
using System;
using System.Numerics;
using static System.Console;

namespace rogueLike
{
    internal class Game
    {
        private int FrameCount = 0,
                    level = 1,
                    life = 3;
        private World myWorld;
        private bool GameStatus = false;

        public Game()
        {
            myWorld = new World(level);
        }

        public void Start()
        {
            GameStatus = true;
            Loop();
        }

        public void Loop()
        {
            while (GameStatus)
            {
                Drawer.DrawFrame(myWorld.GetFrame(), 
                    myWorld.GetPlayer(), 
                    myWorld.GetZombies(), 
                    myWorld.GetArchers(),
                    myWorld.GetAllArrows());
                Drawer.DrawGameStats(40, FrameCount);
                HandleEntityAction();
                GameStatus = !IsGoal();
                FrameCount++;
                System.Threading.Thread.Sleep(1);
            }
            LevelUp();
        }

        void HandleEntityAction()
        {
            HandlePlayerInput(myWorld.GetPlayer());
            HandleEnemyAction(myWorld.GetZombies(), myWorld.GetArchers());


            var arrows = myWorld.GetAllArrows();

            if (arrows is not null && FrameCount % 15 == 0)
            foreach(var arrow in arrows)
            {
                if(arrow.Position != Vector2.Zero)
                arrow.Move();
                TryToHit(arrow.Position, arrow.GetSymbol());
            }

            if (arrows is not null)
            for (int i = 0; i < arrows.Count; i++)
            {
                    if (arrows[i].Position == Vector2.Zero)
                    myWorld.RemoveArrow(arrows[i]);
            }

        }

        public void RestartLevel()
        {
            life--;
            myWorld.GenerateMaze(level);
            myWorld.GetAllArrows().Clear(); 
            Start();
        }

        private bool IsGoal()
        {
            GameObject elementAtPlayerPos = myWorld.GetElementAt(myWorld.GetPlayer().GetPos());
            if (myWorld.GetPlayer().Position == Vector2.Zero || life > 0)
                RestartLevel();
            return World.CompareObjects(elementAtPlayerPos, new ExitDoor());
        }

        private void LevelUp()
        {
            level++;
            myWorld.GenerateMaze(level);
            Start();
        }

        public void HandlePlayerInput(Player currentPlayer)
        {
            while (KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = ReadKey(true);
                ConsoleKey key = keyInfo.Key;
                HandleMoveInput(currentPlayer, key);
                HandleAttackInput(currentPlayer, key);
            }
        }

        public void HandleMoveInput(Player currentPlayer, ConsoleKey key)
        {
            if (FrameCount - currentPlayer.lastActionFrame > currentPlayer.MoveCooldown)
            {
                var temp = currentPlayer.Position;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        currentPlayer.Move(Direction.Up, myWorld);
                        break;
                    case ConsoleKey.DownArrow:
                        currentPlayer.Move(Direction.Down, myWorld);
                        break;
                    case ConsoleKey.RightArrow:
                        currentPlayer.Move(Direction.Right, myWorld);
                        break;
                    case ConsoleKey.LeftArrow:
                        currentPlayer.Move(Direction.Left, myWorld);
                        break;
                }

                if (temp != currentPlayer.Position)
                    currentPlayer.lastActionFrame = FrameCount;
            }
        }

        public void HandleAttackInput(Player currentPlayer, ConsoleKey key)
        {
            Vector2 attackPos = Vector2.Zero;
            if (FrameCount - currentPlayer.lastActionFrame > currentPlayer.AttackCooldown)
            {
                switch (key)
                {
                    case ConsoleKey.W:
                        attackPos = currentPlayer.Attack(Direction.Up, myWorld.GetGameObjectGrid());
                        break;
                    case ConsoleKey.S:
                        attackPos = currentPlayer.Attack(Direction.Down, myWorld.GetGameObjectGrid());
                        break;
                    case ConsoleKey.D:
                        attackPos = currentPlayer.Attack(Direction.Right, myWorld.GetGameObjectGrid());
                        break;
                    case ConsoleKey.A:
                        attackPos = currentPlayer.Attack(Direction.Left, myWorld.GetGameObjectGrid());
                        break;
                }
                currentPlayer.lastActionFrame = FrameCount;
                TryToHit(attackPos, '*');
            }
        }

        public void HandleEnemyAction(Zombie[] zombs, Archer[] archs)
        {
            foreach (var zombie in zombs)
            {
                zombie.FindPlayer(myWorld.GetPlayer().Position, myWorld.GetGameObjectGrid());
                EnemyMovement(zombie);
                MeleeEnemyAttackment(zombie);
            }

            foreach (var archer in archs)
            {
                archer.FindPlayer(myWorld.GetPlayer().Position, myWorld.GetGameObjectGrid());
                if(!(myWorld.GetPlayer().Position.X == archer.Position.X)
                    && !(myWorld.GetPlayer().Position.Y == archer.Position.Y))
                EnemyMovement(archer);

                if(EnemyObsPlayer(archer) != Direction.None)
                RangeEnemyAttackment(archer);
            }
        }

        public Direction EnemyObsPlayer(Enemy enemy)
        {
            var nextStep = enemy.GetNextStep(myWorld.GetPlayer().Position) - enemy.Position;

            if (nextStep == Vector2.UnitX)
                return Direction.Down;
            else
            if (nextStep == -Vector2.UnitX)
                return Direction.Up;
            else
            if (nextStep == Vector2.UnitY)
                return Direction.Right;
            else
            if (nextStep == -Vector2.UnitY)
                return Direction.Left;
            else
                return Direction.None;
        }

        public void EnemyMovement(Enemy enemy)
        {
            var direct = EnemyObsPlayer(enemy);

            if (FrameCount - enemy.lastActionFrame > enemy.MoveCooldown && direct != Direction.None)
            {
                var temp = enemy.Position;
                switch (direct)
                {
                    case Direction.Left:
                        enemy.Move(direct, myWorld);
                        break;
                    case Direction.Right:
                        enemy.Move(direct, myWorld);
                        break;
                    case Direction.Down:
                        enemy.Move(direct, myWorld);
                        break;
                    case Direction.Up:
                        enemy.Move(direct, myWorld);
                        break;
                }

                if (temp != enemy.Position)
                    enemy.lastActionFrame = FrameCount;
            }
        }

        private void RangeEnemyAttackment(Archer archer)
        {
            var direct = Direction.None;
            var playerPos = myWorld.GetPlayer().Position;

            if(archer.Position.X > playerPos.X && archer.Position.Y == playerPos.Y)
                direct = Direction.Up;
            else
            if (archer.Position.X < playerPos.X && archer.Position.Y == playerPos.Y)
                direct = Direction.Down;
            else
            if (archer.Position.Y < playerPos.Y && archer.Position.X == playerPos.X)
                direct = Direction.Right;
            else
            if (archer.Position.Y > playerPos.Y && archer.Position.X == playerPos.X)
                direct = Direction.Left;

            if (FrameCount - archer.lastActionFrame > archer.AttackCooldown && direct != Direction.None)
            {
                var Arrow = new Arrow(archer.GetPos(), direct, myWorld.GetGameObjectGrid());
                myWorld.AddArrow(Arrow);
                archer.lastActionFrame = FrameCount;
            }
        }

        public void MeleeEnemyAttackment(Enemy enemy)
        {
            var playerPos = myWorld.GetPlayer().Position;
            var attackPos = Vector2.Zero;
            if (FrameCount - enemy.lastActionFrame > enemy.AttackCooldown)
            {
                if (enemy.Position + Vector2.UnitX == playerPos)
                    attackPos = enemy.Attack(Direction.Down, myWorld.GetGameObjectGrid());
                else
                if (enemy.Position - Vector2.UnitX == playerPos)
                    attackPos = enemy.Attack(Direction.Up, myWorld.GetGameObjectGrid());
                else
                if (enemy.Position + Vector2.UnitY == playerPos)
                    attackPos = enemy.Attack(Direction.Right, myWorld.GetGameObjectGrid());
                else
                if (enemy.Position - Vector2.UnitY == playerPos)
                    attackPos = enemy.Attack(Direction.Left, myWorld.GetGameObjectGrid());

                enemy.lastActionFrame = FrameCount;
                if (attackPos != Vector2.Zero)
                    TryToHit(attackPos, '*');
            }
        }

        public void TryToHit(Vector2 attackPos, Char hitSymbol)
        {
            GameObject objectAt = myWorld.GetGameObjectGrid()[(int)attackPos.X, (int)attackPos.Y];
            Creature? attackedObj = objectAt as Creature;

            if (attackPos != Vector2.Zero)
                Drawer.DrawAttack(attackPos, hitSymbol);

            if (attackedObj != null)
            {
                attackedObj.Dead(myWorld);
            }
        }

    }
}


