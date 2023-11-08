using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Numerics;
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
        private int updateRate = 0;
        private int level = 1;
        private int life = 3;
        private int numberOfZomb = 1, numberOfArch = 1;
        private bool isChasing = false;
        public Game()
        {
            myWorld = new World();
            currentPlayer = new Player(myWorld.GetPlayerSpawnPos());
        }

        public void Start()
        {
            Drawer.DrawIntro();
            SpawnEnemy();
            Loop();
        }

        public void Loop()
        {
            while (PlayerStatus() == 0)
            {
                ChasePlayer();
                Drawer.DrawFrame(myWorld, currentPlayer, zombie, archer);
                Drawer.DrawGameStats(myWorld.GetGrid().GetLength(0) - 1,
                    updateRate,
                    level,
                    life
                    );
                updateRate++;
                HandlePlayerInput();
                System.Threading.Thread.Sleep(20);
            }
            GameStatus();
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
            return touched;
        }

        private bool IsReachedGoal()
        {
            string elementAtPlayerPos = myWorld.GetElementAt(currentPlayer.GetPos());

            return elementAtPlayerPos == "X" ? true : false;
        }

        private void LevelUp()
        {
            level++;
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
                zombie[i].FindPath(myWorld.GetGrid(), currentPlayer.GetPos());

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

        private void ChasePlayer()
        {
            foreach (var z in zombie)
            {
                if(updateRate % 15 == 0)
                    z.ChasePlayer(currentPlayer.GetPos(), myWorld);
            }
            foreach (var a in archer)
            {
                if (updateRate % 20 == 0)
                    a.ChasePlayer(currentPlayer.GetPos(), myWorld);
                if(updateRate % 50 == 0)
                {
                    a.fireArrow(myWorld);
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
                        if (myWorld.isPosWalkable(PlayerX, PlayerY - 1))
                            currentPlayer.SetPos(PlayerY - 1, PlayerX);
                        break;
                    case ConsoleKey.DownArrow:
                        if (myWorld.isPosWalkable(PlayerX, PlayerY + 1))
                            currentPlayer.SetPos(PlayerY + 1, PlayerX);
                        break;
                    case ConsoleKey.RightArrow:
                        if (myWorld.isPosWalkable(PlayerX + 1, PlayerY))
                            currentPlayer.SetPos(PlayerY, PlayerX + 1);
                        break;
                    case ConsoleKey.LeftArrow:
                        if (myWorld.isPosWalkable(PlayerX - 1, PlayerY))
                            currentPlayer.SetPos(PlayerY, PlayerX - 1);
                        break;
                    default:
                        break;
                }
            }
        }


    }
}
