using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace rogueLike
{
    internal class Player
    {
        public static int X {  get; set; }
        public static int Y { get; set; }
        protected string PlayerMarker;
        protected ConsoleColor PlayerColor;

        public Player()
        {
            X = PlayerPosGenerator(Maze.Grid);
            Y = 0;
            PlayerMarker = "o";
            PlayerColor = ConsoleColor.Green;
        }

        public void Draw()
        {
            ForegroundColor = this.PlayerColor;
            SetCursorPosition(X, Y);
            Write(this.PlayerMarker);
            ResetColor();
        }
        
        public virtual int PlayerPosGenerator(String[,] grid)
        {
            int Xmark = 0;
            int PosX = 0;
            Random rnd = new Random();
            for (int x = 0; x < grid.GetLength(1); x++)
                {
                    if(grid[grid.GetLength(0) - 1,x] == "X")
                    {
                    Xmark = x;
                    break;
                    }
                }

            if(Xmark < grid.GetLength(1)/2)
            {
              PosX = rnd.Next(grid.GetLength(1) / 2, grid.GetLength(1) - 1);
            }
            else PosX = rnd.Next(1, grid.GetLength(1) / 2);


            if (grid[1, PosX] != constants.wall)
                return PosX;
            else
                return PosX + 1;
        }
    }
}
