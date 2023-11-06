using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
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
        private Maze maze;
        private Zombie[] zombie;
        private List<int[]> spawnMap = new List<int[]>();
        private int updateRate = 0;
        private int level = 1;
        private int sizeX = 20;
        private int sizeY = 20;

        public void Start()
        {
            maze = new Maze(sizeY, sizeX);
            myWorld = new World(maze.GetGrid());
            currentPlayer = new Player(PlayerPosGenerator());

            EnemySpawner();
            Drawer.DrawIntro();

            CheckGameStatus();
        }

        public bool Loop()
        {
            while (!GameEndChecker())
            {
                if (IsPlayerALife())
                {
                    break;
                }
                ChasePlayer();
                Drawer.DrawFrame(myWorld, currentPlayer, zombie);
                Drawer.DrawGameStats(maze.GetGrid().GetLength(0) - 1, updateRate, level);
                updateRate++;
                HandlePlayerInput();
                System.Threading.Thread.Sleep(20);
            }
            LevelUp();
            return true;
        }

        private void EnemySpawner()
        {
            zombie = new Zombie[level * 2];
            spawnMap = GenerateSpawnMap();
            Random rnd = new Random();
            for (int i = 0; i < zombie.Length; i++)
            {
                int random = rnd.Next(0, spawnMap.Count - 1);
                int X = spawnMap[random][0];
                int Y = spawnMap[random][1];
                spawnMap.Remove(spawnMap[random]);
                zombie[i] = new Zombie(new Vector2(Y, X));
            }
        }

        private void CheckGameStatus()
        {
            if (Loop())
            {
                Drawer.DrawOutroWin();
                Start();
            }
            else
            {
                Drawer.DrawOutroLose();
            }
        }

        public void HandlePlayerInput()
        {
            int PlayerX = (int)currentPlayer.GetPos().Y;
            int PlayerY = (int)currentPlayer.GetPos().X;

            ConsoleKeyInfo keyInfo;
            if (KeyAvailable)
            {
                keyInfo = ReadKey(true);
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

        public int PlayerPosGenerator()
        {
            int Xmark = 0;
            int PosX = 0;
            String[,] grid = maze.GetGrid();
            Random rnd = new Random();
            for (int x = 0; x < grid.GetLength(1); x++)
            {
                if (grid[grid.GetLength(0) - 1, x] == "X")
                {
                    Xmark = x;
                    break;
                }
            }

            if (Xmark < grid.GetLength(1) / 2)
            {
                PosX = rnd.Next(grid.GetLength(1) / 2, grid.GetLength(1) - 1);
            }
            else PosX = rnd.Next(1, grid.GetLength(1) / 2);


            if (grid[1, PosX] != "█")
                return PosX;
            else
                return PosX + 1;
        }

        private bool IsPlayerALife()
        {
            bool touched = false;
            for (int i = 0; i < zombie.Length; i++)
            {
                touched = zombie[i].IsTouchPlayer(currentPlayer.GetPos());
                if(touched)
                {
                    break;
                }
            }
            return touched;
        }

        private bool GameEndChecker()
        {
            string elementAtPlayerPos = myWorld.GetElementAt(currentPlayer.GetPos());

            return elementAtPlayerPos == "X" ? true : false;
        }

        private void LevelUp()
        {
            level++;
            sizeX += 15;
            sizeY += 2;
        }

        private List<int[]> GenerateSpawnMap()
        {
            List<int[]> spawnMap = new List<int[]>();
            int numberOfSpawnCells = 0;
            Random rnd = new Random();
            for (int y = 0; y < maze.GetGrid().GetLength(0); y++)
                for (int x = 0; x < maze.GetGrid().GetLength(1); x++)
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


        private void ChasePlayer()
        {

            foreach (var z in zombie)
            {
                if (z.IsSeeThePlayer(currentPlayer.GetPos(), maze.GetGrid()))
                {
                    if (updateRate % 10 == 0)
                    {
                        List<Point> path = new List<Point>();
                        path = PathFinder.FindPath(
                            maze.GetGrid(),
                            new Point((int)z.GetPos().X, (int)z.GetPos().Y),
                            new Point((int)currentPlayer.GetPos().Y, (int)currentPlayer.GetPos().X));
                        z.SetPos(path[1].X, path[1].Y);
                    }
                    z.color = ConsoleColor.DarkRed;
                }
                else z.color = ConsoleColor.Red;
            }
        }
    }
}
