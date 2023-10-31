using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
namespace rogueLike
{
    internal class World
    {
        private String ground = constants.ground;
        private String room = constants.room;
        private int Rows = 0;
        private int Cols = 0;
        private String frame = "";

        public World()
        {
            Rows = Maze.Grid.GetLength(0);
            Cols = Maze.Grid.GetLength(1);
            GetFrame();
        }

        private void GetFrame()
        {
            for (int y = 0; y < Rows; y++)
            {
                for (int x = 0; x < Cols; x++)
                {
                    frame += Maze.Grid[y, x];
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
            return Maze.Grid[y, x] == ground || Maze.Grid[y, x] == "X" || Maze.Grid[y, x] == room;
        }

        public string GetElementAt(int x, int y)
        {
            return Maze.Grid[y, x];
        }
    }
}
