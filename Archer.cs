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
        private bool isShooted = false;
        Arrow arrow;

        public Archer(Vector2 spawnPos) : base(spawnPos)
        {
            SetColor(ConsoleColor.Blue);
            SetMarker("A");
            SetViewDistance(8);
        }

        public void DrawArrow()
        {
            if(isShooted)
            arrow.Draw();
        }

        public void FireArrow(int direction)
        {
            if(!isShooted)
                arrow = new Arrow(GetPos(), direction);
            isShooted = true;
        }

        private Vector2 GetArrowPos()
        {
            if (arrow != null)
                return arrow.GetPos();
            else 
                return new Vector2(-1,-1);
        }

        public void UpdateArrowPos(World myWorld)
        {
            if (isShooted)
                arrow.UpdatePos(GetPos(), myWorld);
            RefreshArrow();
        }

        public bool ArrowHitPlayer(Vector2 playerPos)
        {
            if (GetArrowPos() == new Vector2(playerPos.Y, playerPos.X))
                return true;
            else
                return false;
        }

        public void RefreshArrow()
        {
            if (arrow != null)
                if (arrow.GetPos() == GetPos())
                    isShooted = false;
        }

        private void spotPlayerDirection(Vector2 PlayerPos)
        {
            down = GetPos().Y < PlayerPos.X;
            up = GetPos().Y > PlayerPos.X;
            left = GetPos().X > PlayerPos.Y;
            right = GetPos().X < PlayerPos.Y;
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

            if(up && (!left && !right))
                FireArrow(1);
            if (down && (!left && !right))
                FireArrow(2);
            if (left && (!up && !down))
                FireArrow(3);
            if (right && (!up && !down))
                FireArrow(4);
        }
        private void Escape(Vector2 playerPos, World myWorld)
        {
            int X = (int)GetPos().X;
            int Y = (int)GetPos().Y;

            spotPlayerDirection(playerPos);

            if (up && myWorld.isPosWalkable(X, Y + 1))
            {
                SetPos(X, Y + 1);
            }
            if (right && myWorld.isPosWalkable(X - 1, Y))
            {
                SetPos(X - 1, Y);
            }
            if (left && myWorld.isPosWalkable(X + 1, Y))
            {
                SetPos(X + 1, Y);
            }
            if (down && myWorld.isPosWalkable(X, Y - 1))
            {
                SetPos(X, Y - 1);
            }
        }
    }
}
