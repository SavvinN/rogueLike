using rogueLike.GameObjects;
using rogueLike.GameObjects.Items;
using rogueLike.GameObjects.MazeObjects;
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
            while(GameStatus)
            {
                Drawer.DrawFrame(myWorld.GetFrame(), myWorld.GetPlayer(), myWorld.GetZombies(), myWorld.GetArchers());
                Drawer.DrawGameStats(40, FrameCount, myWorld.GetPlayer().currentStamina);
                HandelPlayer(myWorld.GetPlayer());
                GameStatus = !isGoal();
                FrameCount++;
                System.Threading.Thread.Sleep(1);
            }
            LevelUp();
        }

        private bool isGoal()
        {
            GameObject elementAtPlayerPos = myWorld.GetElementAt(myWorld.GetPlayer().GetPos());
            return myWorld.CompareObjects(elementAtPlayerPos, new ExitDoor());
        }

        private void LevelUp()
        {
            level++;
            myWorld.GenerateMaze(level);
            Start();
        }

        public void HandelPlayer(Player currentPlayer)
        {
            currentPlayer.RecoverStamina();
            Vector2 attackPos = Vector2.Zero;
            while (KeyAvailable)
            {
                int PlayerX = (int)currentPlayer.GetPos().Y;
                int PlayerY = (int)currentPlayer.GetPos().X;
                ConsoleKeyInfo keyInfo = ReadKey(true);
                ConsoleKey key = keyInfo.Key;
                HandleMoveInput(currentPlayer, key, PlayerX, PlayerY);
                attackPos = HandleAttackInput(currentPlayer, key, PlayerX, PlayerY);

                if(attackPos !=  Vector2.Zero)
                Drawer.DrawAttack(attackPos);
            }
        }

        public void HandleMoveInput(Player currentPlayer, ConsoleKey key, int PlayerX, int PlayerY)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    if (myWorld.IsPosWalkable(new(PlayerY - 1, PlayerX)))
                        currentPlayer.Move(Direction.Up);
                    break;
                case ConsoleKey.DownArrow:
                    if (myWorld.IsPosWalkable(new(PlayerY + 1, PlayerX)))
                        currentPlayer.Move(Direction.Down);
                    break;
                case ConsoleKey.RightArrow:
                    if (myWorld.IsPosWalkable(new(PlayerY, PlayerX + 1)))
                        currentPlayer.Move(Direction.Right);
                    break;
                case ConsoleKey.LeftArrow:
                    if (myWorld.IsPosWalkable(new(PlayerY, PlayerX - 1)))
                        currentPlayer.Move(Direction.Left);
                    break;
            }
        }

        public Vector2 HandleAttackInput(Player currentPlayer, ConsoleKey key, int PlayerX, int PlayerY)
        {
            switch (key)
            {
                case ConsoleKey.W:
                    if (!myWorld.IsPosWall(new(PlayerY - 1, PlayerX)))
                        return currentPlayer.Attack(Direction.Up);
                    break;
                case ConsoleKey.S:
                    if (!myWorld.IsPosWall(new(PlayerY + 1, PlayerX)))
                        return currentPlayer.Attack(Direction.Down);
                    break;
                case ConsoleKey.D:
                    if (!myWorld.IsPosWall(new(PlayerY, PlayerX + 1)))
                        return currentPlayer.Attack(Direction.Right);
                    break;
                case ConsoleKey.A:
                    if (!myWorld.IsPosWall(new(PlayerY, PlayerX - 1)))
                        return currentPlayer.Attack(Direction.Left);
                    break;
            }
            return Vector2.Zero;
        }
    }
}


