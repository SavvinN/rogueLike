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
        protected string PlayerMarker;
        protected ConsoleColor PlayerColor;
        private Position position = new Position();

        public Player(int PosX)
        { 
            position.Pos = new Vector2(PosX, 0);
            PlayerMarker = "o";
            PlayerColor = ConsoleColor.Green;
        }

        public void SetPos(int Y, int X)
        {
            position.Pos = new Vector2 (Y, X);
        }

        public Vector2 GetPos()
        {
            return position.Pos;
        }

        public void Draw()
        {
            int Y = (int)position.Pos.Y;
            int X = (int)position.Pos.X;
            ForegroundColor = this.PlayerColor;
            SetCursorPosition(Y, X);
            Write(this.PlayerMarker);
            ResetColor();
        }
        
        // Генерация позиции игрока, генерирует игрока в другой части лабиринта
    }
}
