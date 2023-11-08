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
    internal class Arrow
    {
        private int _direction, distance;
        private String mark = "*";
        private Position position = new Position();
        private ConsoleColor color = ConsoleColor.DarkCyan;

        public Arrow(int direction, Vector2 pos, World myWorld)
        {
            mark = "*";
            position.Pos = pos;
            Move(direction, myWorld);
        }


        private void Move(int direction, World myWorld)
        {
            int X = (int)GetPos().X;
            int Y = (int)GetPos().Y;

            if (direction == 1 && myWorld.isPosWalkable(X, Y + 1))
            {
                SetPos(X, Y + 1);
            }

            if (direction == 2 && myWorld.isPosWalkable(X + 1, Y))
            {
                SetPos(X + 1, Y);
            }

            if (direction == 3 && myWorld.isPosWalkable(X - 1, Y))
            {
                SetPos(X - 1, Y);
            }

            if (direction == 4 && myWorld.isPosWalkable(X, Y - 1))
            {
                SetPos(X, Y - 1);
            }

        }

        public void isVisible(bool visible) => 
            mark = !visible ? "" : "*";

        public void Draw()
        {
            ForegroundColor = color;
            SetCursorPosition((int)GetPos().X, (int)GetPos().Y);
            Write(mark);
            ResetColor();
        }

        public void SetPos(int Y, int X) =>
        position.Pos = new Vector2(Y, X);

        public void SetPos(Vector2 Pos) =>
            position.Pos = Pos;

        public Vector2 GetPos() => position.Pos;
    }
}
