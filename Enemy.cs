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
        private Position position = new Position();

        public Enemy()
        {
            Spawn();
        }
        public void Draw()
        {
            ForegroundColor = color;
            SetCursorPosition(X, Y);
            Write(enemyMark);
            ResetColor();
        }
        
        private void Spawn()
        {

        }

        private void Move()
        {
            
        }

        private bool IsSeeThePlayer(int PlayerY, int PlayerX)
        {
            for (int y = 0; y < viewDistance; y++)
            {
                for (int x = 0; x < viewDistance; x++)
                {
                    if ((Y + y == PlayerY && X + x == PlayerX) 
                        || (Y - y == PlayerY && X - x == PlayerX)
                        || (Y + y == PlayerY && X - x == PlayerX)
                        || (Y - y == PlayerY && X + x == PlayerX))
                    {
                        return true;
                    }
                    else
                        return false;
                }
            }
            return false;
        }

        private bool IsTouchPlayer(int PlayerY, int PlayerX)
        {
            return PlayerX == X && PlayerY == Y;
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
