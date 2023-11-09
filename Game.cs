using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace rogueLike
{
    internal class Game
    {

        private World myWorld;
        private Player currentPlayer;
        private Zombie[] zombie;
        private Archer[] archer;
        private Key _key = new Key();
        private Heart heart = new Heart();
        private Vector2 HitPos;
        private int updateRate = 0, level = 1, life = 3, cooldown = 0;
        private int numberOfZomb = 1, numberOfArch = 1;
        private bool isChasing = false, isHit = false, key = false;
        public Game()
        {
            myWorld = new World();
            currentPlayer = new Player(myWorld.GetPlayerSpawnPos());
        }

        public void Start()
        {
            numberOfArch = level / 2;
            numberOfZomb = level;
            Drawer.DrawIntro();
            _key.RemoveKey();
            SpawnEnemy();
            if(level % 3 == 0)
            {
                heart.SpawnHeart(myWorld.ItemSpawnMap());
            }
            Loop();
        }

        public void Loop()
        {
            while (PlayerStatus() == 0)
            {
                Drawer.DrawFrame(myWorld, currentPlayer, zombie, archer);
                HandlePlayerInput();
                HitTheEnemy();
                UpdateEnemys();
                Drawer.DrawItems(_key, heart);
                Drawer.DrawGameStats(myWorld.GetGrid().GetLength(0) - 1,
                    updateRate,
                    level,
                    life
                    );
                updateRate++;
                System.Threading.Thread.Sleep(20);
            }
            GameStatus();
        }

        public void DrawHit()
        {
            cooldown++;
            if (myWorld.isPosWalkable((int)HitPos.X, (int)HitPos.Y))
            {
                if (isHit)
                {
                    if (cooldown > 40)
                    {
                        SetCursorPosition((int)HitPos.X, (int)HitPos.Y);
                        Write("/");
                        isHit = false;
                        cooldown = 0;
                    }
                    isHit = false;
                }
                else if (!isHit)
                {
                    HitPos = new Vector2(1, 1);
                    SetCursorPosition(0, 0);
                    Write("");
                }
            }
            else
            {
                isHit = false;
            }
        }


        public void HitTheEnemy()
        {
            DrawHit();

            foreach (var z in zombie)
            {
                if(HitPos == z.GetPos())
                {
                    numberOfZomb--;
                    DropKey(z);
                    z.Dead();
                }
            }

            foreach (var a in archer)
            {
                if(HitPos == a.GetArrowPos())
                {
                    a.RemoveArrow();
                }
                if (HitPos == a.GetPos())
                {
                    numberOfArch--;
                    DropKey(a);
                    a.Dead();
                }
            }
        }

        private void DropKey(Enemy a)
        {
            int enemyAlife = numberOfZomb + numberOfArch;
            Random rnd = new Random();
            if (a.GetPos() != new Vector2(0, 0) && (rnd.NextSingle() > 0.9 || enemyAlife < 1))
                _key.SpawnKey(a.GetPos());
        }

        private int PlayerStatus() =>
                IsReachedGoal()
                ? 1
                : IsUnderAttack()
                ? 2
                : 0;

        private void GameStatus()
        {
            switch (PlayerStatus())
            {
                case 1:
                    LevelUp();
                    Start();
                    break;
                case 2:
                    currentPlayer.SetPos(myWorld.GetPlayerSpawnPos());
                    life--;
                    if (life >= 0)
                        Start();
                    else
                        Drawer.DrawOutroLose();
                    break;
                default:
                    break;
            }
        }

        private bool IsUnderAttack()
        {
            bool touched = false;
            for (int i = 0; i < zombie.Length; i++)
            {
                touched = zombie[i].IsTouchPlayer(currentPlayer.GetPos());
                if (touched)
                {
                    break;
                }
            }

            foreach(var a in archer)
            {
                if(a.ArrowHitPlayer(currentPlayer.GetPos()))
                {
                    touched = true;
                    break;
                }
            }
            return touched;
        }

        private bool IsReachedGoal()
        {
            string elementAtPlayerPos = myWorld.GetElementAt(currentPlayer.GetPos());

            return elementAtPlayerPos == "X" && _key.GetExist() ? true : false;
        }

        private void LevelUp()
        {
            level++;
            numberOfArch = level / 2;
            numberOfZomb = level;
            myWorld.RegenerateMaze(level);
            currentPlayer.SetPos(myWorld.GetPlayerSpawnPos());
        }

        private List<int[]> GenerateSpawnMap()
        {
            List<int[]> spawnMap = new List<int[]>();
            int numberOfSpawnCells = 0;
            Random rnd = new Random();
            for (int y = 0; y < myWorld.GetGrid().GetLength(0); y++)
                for (int x = 0; x < myWorld.GetGrid().GetLength(1); x++)
                {
                    {
                        if (myWorld.IsSpawnable(y, x))
                        {
                            int[] YX = { y, x };
                            numberOfSpawnCells++;
                            spawnMap.Add(YX);
                        }
                    }
                }
            return spawnMap;
        }

        private void SpawnEnemy()
        {
            List<int[]> spawnMap = new List<int[]>();
            zombie = new Zombie[numberOfZomb];
            archer = new Archer[numberOfArch];
            spawnMap = GenerateSpawnMap();
            Random rnd = new Random();
            for (int i = 0; i < zombie.Length; i++)
            {
                zombie[i] = new Zombie(GetSpawnPos(spawnMap));
            }
            for (int i = 0; i < archer.Length; i++)
            {
                archer[i] = new Archer(GetSpawnPos(spawnMap));
            }
        }

        private Vector2 GetSpawnPos(List<int[]> spawnMap)
        {
            Random rnd = new Random();
            int random = rnd.Next(0, spawnMap.Count - 1);
            int X = spawnMap[random][0];
            int Y = spawnMap[random][1];
            return new Vector2(Y, X);
        }

        private void UpdateEnemys()
        {
            foreach (var z in zombie)
            {
                if(updateRate % 15 == 0)
                    z.ChasePlayer(currentPlayer.GetPos(), myWorld);
            }
            foreach (var a in archer)
            {
                if (updateRate % 30 == 0)
                    a.ChasePlayer(currentPlayer.GetPos(), myWorld);
                if (updateRate % 10 == 0)
                {
                    a.UpdateArrowPos(myWorld);
                }
                          
            }
        }

        public void HandlePlayerInput()
        {
            int PlayerX = (int)currentPlayer.GetPos().Y;
            int PlayerY = (int)currentPlayer.GetPos().X;
            if (KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = ReadKey(true);
                ConsoleKey key = keyInfo.Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        if (myWorld.isPosWalkable(PlayerX, PlayerY - 1) && !isHit)
                            currentPlayer.SetPos(PlayerY - 1, PlayerX);
                        break;
                    case ConsoleKey.DownArrow:
                        if (myWorld.isPosWalkable(PlayerX, PlayerY + 1) && !isHit)
                            currentPlayer.SetPos(PlayerY + 1, PlayerX);
                        break;
                    case ConsoleKey.RightArrow:
                        if (myWorld.isPosWalkable(PlayerX + 1, PlayerY) && !isHit)
                            currentPlayer.SetPos(PlayerY, PlayerX + 1);
                        break;
                    case ConsoleKey.LeftArrow:
                        if (myWorld.isPosWalkable(PlayerX - 1, PlayerY) && !isHit)
                            currentPlayer.SetPos(PlayerY, PlayerX - 1);
                        break;

                    case ConsoleKey.W:
                        if (myWorld.isPosWalkable(PlayerX, PlayerY - 1))
                            HitPos = new Vector2(PlayerX , PlayerY - 1);
                        isHit = true;
                        break;
                    case ConsoleKey.S:
                        if (myWorld.isPosWalkable(PlayerX, PlayerY + 1))
                            HitPos = new Vector2(PlayerX, PlayerY + 1);
                        isHit = true;
                        break;
                    case ConsoleKey.A:
                        if (myWorld.isPosWalkable(PlayerX - 1, PlayerY))
                            HitPos = new Vector2(PlayerX - 1, PlayerY);
                        isHit = true;
                        break;
                    case ConsoleKey.D:
                        if (myWorld.isPosWalkable(PlayerX + 1, PlayerY))
                            HitPos = new Vector2(PlayerX + 1, PlayerY);
                        isHit = true; ;
                        break;
                    default:
                        break;
                }
            }
            ItemManager();
        }

        private void ItemManager()
        {
            heart.PickUp(currentPlayer.GetPos());
            _key.PickUp(currentPlayer.GetPos());
            if (heart.GetExist())
            {
                life++;
                heart.SetExist(false);
            }
        }


    }
}
