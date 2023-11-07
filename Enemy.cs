using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace rogueLike
{
    internal class Enemy
    {
        public ConsoleColor color;
        private string enemyMark = "Z";
        private Position position = new Position();
        private int viewDistance = 5;

        public Enemy(Vector2 spawnPos)
        {
            Spawn(spawnPos);
        }

        public void SetPos(int Y, int X)
        {
            position.Pos = new Vector2(Y, X);
        }

        public void SetPos(Vector2 Pos)
        {
            position.Pos = Pos;
        }

        public Vector2 GetPos()
        {
            return position.Pos;
        }

        public void Draw()
        {
            ForegroundColor = color;
            SetCursorPosition((int)GetPos().X, (int)GetPos().Y);
            Write(enemyMark);
            ResetColor();
        }
        
        public void Spawn(Vector2 spawnPos)
        {
            SetPos(spawnPos);
        }

        private void MovePatrol()
        {
            
        }

        public bool IsSeeThePlayer(Vector2 PlayerPosition, String[,] maze)
        {
            int k = 0;
            bool walled = false;
            int x = (int)GetPos().Y;
            int y = (int)GetPos().X;
            while (PlayerPosition != new Vector2(x,y))
            {
                if (PlayerPosition.X != x)
                    x += PlayerPosition.X > x ? 1 : -1;
                if(PlayerPosition.Y != y)
                    y += PlayerPosition.Y > y ? 1 : -1;
                k++;
                walled = maze[x,y] == "█"? true : true;
            }
            SetCursorPosition(60, 20);
            Write(k);
            return viewDistance > k;
        }

        public bool IsTouchPlayer(Vector2 playerPos)
        {
            return playerPos == new Vector2 (GetPos().Y, GetPos().X);
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
