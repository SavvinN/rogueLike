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
        private int _direction;
        private String mark = "*";
        private Position position = new Position();
        private ConsoleColor color = ConsoleColor.DarkCyan;

        public Arrow(int direction, Vector2 pos)
        {
            position.Pos = pos;
        }

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
