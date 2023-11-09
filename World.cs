using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
namespace rogueLike
{
    internal class World
    {
        private String ground = " ", room = ".", wall = "█";
        private int Rows = 0, Cols = 0;
        private String[,] _Grid = { {""} };
        private String frame = "";
        private int sizeX = 20, sizeY = 18;
        private int _level = 0;
        private Maze maze;
        public World()
        {
            _level = 1;
            increaseSizeOfWorld();
            maze = new Maze(sizeY, sizeX, wall, ground, room);
            _Grid = maze.GetGrid();
            Rows = _Grid.GetLength(0); Cols = _Grid.GetLength(1);
            frame = CreateFrame(_Grid);
        }

        public void RegenerateMaze(int level)
        {
            _level = level;
            increaseSizeOfWorld();
            maze = new Maze(sizeY, sizeX, wall, ground, room);
            _Grid = maze.GetGrid();
            Rows = _Grid.GetLength(0); Cols = _Grid.GetLength(1);
            frame = CreateFrame(_Grid);
        }

        public List<Vector2> ItemSpawnMap()
        {
            List<Vector2> result = new List<Vector2>();
            int wallCount = 0;
            for (int y = 1; y < Cols - 1; y++)
            {
                for (int x = 1; x < Rows - 1; x++)
                {
                    if (_Grid[x - 1, y] == wall)
                        wallCount++;
                    if (_Grid[x + 1, y] == wall)
                        wallCount++;
                    if (_Grid[x, y - 1] == wall)
                        wallCount++;
                    if (_Grid[x, y + 1] == wall)
                        wallCount++;
                    if (wallCount == 3 && _Grid[x, y] == ground)
                    {
                        result.Add(new Vector2(x, y));
                    }
                    wallCount = 0;
                }
            }
            return result;
        }

        public String[,] GetGrid()
        {
            return _Grid;
        }

        private void increaseSizeOfWorld()
        {
            sizeX += _level * 2;
            sizeY ++;
        }

        public String CreateFrame(String[,] Grid)
        {
            String frame = "";
            for (int y = 0; y < Rows; y++)
            {
                for (int x = 0; x < Cols; x++)
                {
                    frame += Grid[y, x] == "." ? " " : Grid[y, x];
                }
                frame += "\n";
            }
            return frame;
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

        public bool isPosWalkable(float X, float Y)
        {
            int x = (int)X;
            int y = (int)Y;
            // Проверка границ
            if (x < 0 || y < 0 || x >= Cols || y >= Rows)
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

        public Vector2 GetPlayerSpawnPos()
        {
            int Xmark = 0;
            int PosX = 0;
            Random rnd = new Random();
            for (int x = 0; x < _Grid.GetLength(1); x++)
            {
                if (_Grid[_Grid.GetLength(0) - 1, x] == "X")
                {
                    Xmark = x;
                    break;
                }
            }

            if (Xmark < _Grid.GetLength(1) / 2)
            {
                PosX = rnd.Next(_Grid.GetLength(1) / 2, _Grid.GetLength(1) - 1);
            }
            else PosX = rnd.Next(1, _Grid.GetLength(1) / 2);


            if (_Grid[1, PosX] != "█")
                return new Vector2(1, PosX);
            else
                return new Vector2(1, PosX + 1);
        }
    }
}
