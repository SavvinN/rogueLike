using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace rogueLike
{
    internal class Archer : Enemy
    {
        private bool up = false;
        private bool down = false;
        private bool right = false;
        private bool left = false;

        public Archer(Vector2 spawnPos) : base(spawnPos)
        {
            SetColor(ConsoleColor.Blue);
            SetMarker("A");
            SetViewDistance(8);
        }

        private void fireArrow()
        {
            if (up)
            {
                Arrow arrow = new Arrow(1, GetPos());
            }
            if (down)
            {
                Arrow arrow = new Arrow(2, GetPos());
            }
            if (right)
            {
                Arrow arrow = new Arrow(3, GetPos());
            }
            if (left)
            {
                Arrow arrow = new Arrow(4, GetPos());
            }
        }

        private void spotPlayerDirection(Vector2 PlayerPos)
        {
            up = GetPos().X < PlayerPos.X;
            down = GetPos().X > PlayerPos.X;
            left = GetPos().Y > PlayerPos.Y;
            right = GetPos().Y < PlayerPos.Y;
        }

        public override void MoveToPlayer(Vector2 playerPos, World myWorld)
        {
            if(path.Count > 5) 
            {
                SetPos(path[1].X, path[1].Y);
            } 
            else
            {
                Escape(playerPos, myWorld);
            }
        }
        private void Escape(Vector2 playerPos, World myWorld)
        {
            int X = (int)GetPos().X;
            int Y = (int)GetPos().Y;

            spotPlayerDirection(playerPos);

            if (up && myWorld.isPosWalkable(X, Y - 1))
            {
                SetPos(X, Y - 1);
            }
            if (right && myWorld.isPosWalkable(X - 1, Y))
            {
                SetPos(X - 1, Y);
            }
            if (left && myWorld.isPosWalkable(X + 1, Y))
            {
                SetPos(X + 1, Y);
            }
            if (down && myWorld.isPosWalkable(X, Y + 1))
            {
                SetPos(X, Y + 1);
            }
        }
    }
}
