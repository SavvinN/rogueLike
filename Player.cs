using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static System.Console;

namespace rogueLike
{
    internal class Player
    {
        private string PlayerMarker;
        private ConsoleColor PlayerColor;
        private Position position = new Position();

        public Player(Vector2 pos)
        {
            SetPos(pos);
            PlayerMarker = "o";
            PlayerColor = ConsoleColor.Green;
        }

        public void SetPos(int Y, int X) =>
            position.Pos = new Vector2(Y, X);

        public void SetPos(Vector2 Pos) =>
            position.Pos = Pos;

        public Vector2 GetPos()
        {
            return position.Pos;
        }

        public void Draw()
        {
            ForegroundColor = this.PlayerColor;
            SetCursorPosition((int)position.Pos.Y, (int)position.Pos.X);
            Write(this.PlayerMarker);
            ResetColor();
        }

    }
}
