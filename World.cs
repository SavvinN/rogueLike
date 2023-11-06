using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
namespace rogueLike
{
    internal class World
    {
        public String ground = " ";
        public String room = ".";
        public int Rows = 0;
        public int Cols = 0;
        public String frame = "";
        private String[,] _Grid = { {""} };
        public World(String[,] Grid)
        {
            _Grid = Grid;
            Rows = Grid.GetLength(0);
            Cols = Grid.GetLength(1);
            GetFrame(Grid);
        }

        public void GetFrame(String[,] Grid)
        {
            for (int y = 0; y < Rows; y++)
            {
                for (int x = 0; x < Cols; x++)
                {
                    frame += Grid[y, x] == "." ? " " : Grid[y, x];
                }
                frame += "\n";
            }
        }

        public void Draw()
        {
            SetCursorPosition(0, 0);
            Write(frame);
        }
        

        public bool isPosWalkable (int x, int y)
        {
            // Проверка границ
            if(x < 0 || y < 0 || x>= Cols|| y>= Rows)
            {
                return false;
            }
            // Проверка стен
            return _Grid[y, x] == ground || _Grid[y, x] == "X" || _Grid[y, x] == room;
        }

        public string GetElementAt(Vector2 Pos)
        {
            return _Grid[(int)Pos.X, (int)Pos.Y];
        }

        public bool IsSpawnable(int y, int x)
        {
            return (_Grid[y, x] == room && _Grid[y - 1, x] == room &&
                        _Grid[y + 1, x] == room && _Grid[y, x - 1] == room &&
                        _Grid[y, x + 1] == room && _Grid[y - 1, x - 1] == room &&
                        _Grid[y + 1, x + 1] == room && _Grid[y + 1, x - 1] == room &&
                        _Grid[y - 1, x + 1] == room);
        }
    }
}
