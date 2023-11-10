using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static System.Console;

namespace rogueLike
{
    internal class Enemy
    {
        private ConsoleColor color;
        private string enemyMark = "Z";
        private Position position = new Position();
        private int viewDistance = 6;
        protected List<Point> path = new List<Point>();
        private bool isChasing = false;
        protected bool isDead = false;

        public Enemy(Vector2 spawnPos)
        {
            
            Spawn(spawnPos);
        }

        protected void SetViewDistance(int dist) => 
            viewDistance = dist;

        protected int GetViewDistance(int dist) => viewDistance;

        public void SetPos(int Y, int X) =>
            position.Pos = new Vector2(Y, X);

        public void SetPos(Vector2 Pos) => 
            position.Pos = Pos;

        public Vector2 GetPos() => position.Pos;

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

        public virtual void MoveToPlayer(Vector2 playerPos, World myWorld)
        {
            SetPos(path[1].X, path[1].Y);
        }

        public void ChasePlayer(Vector2 playerPos, World myWorld)
        {
            if(!isDead)
            {
                FindPath(myWorld.GetGrid(), playerPos);
                if (!isChasing)
                {
                    Patrol(myWorld);
                }

                if (IsSeeThePlayer(playerPos, myWorld.GetGrid()))
                {
                    MoveToPlayer(playerPos, myWorld);
                    isChasing = true;
                }
                else
                    isChasing = false;
            }
        }

        public void FindPath(String[,] grid, Vector2 PlayerPos)
        {
            path = PathFinder.FindPath(
                    grid,
                    new Point((int)GetPos().X, (int)GetPos().Y),
                    new Point((int)PlayerPos.Y, (int)PlayerPos.X));
        }
        public void Patrol(World myWorld)
        {
            Random rnd = Random.Shared;
            int random = rnd.Next(0, 5);
            int zombX = (int)GetPos().X;
            int zombY = (int)GetPos().Y;
            switch (random)
            {
                case 1:
                    if (myWorld.isPosWalkable(zombX - 1, zombY))
                        SetPos(zombX - 1, zombY);
                    break;
                case 2:
                    if (myWorld.isPosWalkable(zombX + 1, zombY))
                        SetPos(zombX + 1, zombY);
                    break;
                case 3:
                    if (myWorld.isPosWalkable(zombX, zombY - 1))
                        SetPos(zombX, zombY - 1);
                    break;
                case 4:
                    if (myWorld.isPosWalkable(zombX, zombY + 1))
                        SetPos(zombX, zombY + 1);
                    break;

            }
        }
        public bool IsSeeThePlayer(Vector2 PlayerPosition, String[,] maze)
        {
            int k = 0;
            int x = (int)GetPos().Y;
            int y = (int)GetPos().X;
            while (PlayerPosition != new Vector2(x,y))
            {
                if (PlayerPosition.X != x)
                    x += PlayerPosition.X > x ? 1 : -1;
                if(PlayerPosition.Y != y)
                    y += PlayerPosition.Y > y ? 1 : -1;
                k++;
            }
            return viewDistance > k;
        }

        public void Dead()
        {
            isDead = true;
            SetMarker("");
            SetPos(0, 0);
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
