using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace rogueLike
{
    internal class Enemy
    {
        private ConsoleColor color;
        private string enemyMark = "Z";
        private int X, Y, viewDistance = 5;
        private String room = constants.room, player = constants.player;

        public Enemy()
        {
            Spawn();
        }
        public void Draw()
        {
            ForegroundColor = color;
            SetCursorPosition(X, Y);
            IsTouchPlayer();
            Write(enemyMark);
            ResetColor();
        }
        
        // Возможно должен быть в контроллере, не смог реализовать запрет спавна в клетке где уже есть объект врага
        private void Spawn()
        {
            List<int[]> spawnMap = new List<int[]>();
            int numberOfSpawnCells = 0;
            Random rnd = new Random();
            for (int y = 0; y < Maze.Grid.GetLength(0); y++)
                for (int x = 0; x < Maze.Grid.GetLength(1); x++)
                {
                    {
                        if (IsSpawnable(y, x))
                        {
                            int[] YX = { y, x };
                            numberOfSpawnCells++;
                            spawnMap.Add(YX);
                        }
                    }
                }
            int i = rnd.Next(0, numberOfSpawnCells - 1);
            X = spawnMap[i][1];
            Y = spawnMap[i][0];
        }

        private bool IsSpawnable(int y, int x)
        {
            return (Maze.Grid[y, x] == room && Maze.Grid[y - 1, x] == room &&
                        Maze.Grid[y + 1, x] == room && Maze.Grid[y, x - 1] == room &&
                        Maze.Grid[y, x + 1] == room && Maze.Grid[y - 1, x - 1] == room &&
                        Maze.Grid[y + 1, x + 1] == room && Maze.Grid[y + 1, x - 1] == room &&
                        Maze.Grid[y - 1, x + 1] == room);
        }

        private void Move()
        {
            
        }

        private void IsSeeThePlayer()
        {
            for (int y = 0; y < viewDistance; y++)
            {
                for (int x = 0; x < viewDistance; x++)
                {
                    if ((Y + y == Player.Y && X + x == Player.X) 
                        || (Y - y == Player.Y && X - x == Player.X)
                        || (Y + y == Player.Y && X - x == Player.X)
                        || (Y - y == Player.Y && X + x == Player.X))
                    {
                        SetCursorPosition(60, 60);
                        WriteLine("IseeThePlayer");
                    }
                }
            }
        }

        private void IsTouchPlayer()
        {
            Game.gameResult = Player.X == X && Player.Y == Y;
        }
        protected void SetColor(ConsoleColor _color)
        {
            color = _color;
        }

        protected void SetMarker(string _mark)
        {
            enemyMark = _mark;
        }


    }

}
