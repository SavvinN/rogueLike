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
        private int _direction, distance = 8;
        private String mark;
        private Position position = new Position();
        private ConsoleColor color = ConsoleColor.DarkCyan;
        private int rate = 0;
        private bool isFligh = false;
        public Arrow(Vector2 pos, int direction)
        {
            isFligh = true;
            mark = "+";
            position.Pos = pos;
            SetDirection(direction);
        }

        public void SetDirection(int direction) 
        { 
            _direction = direction;
        }

        public void Fire(int direction, World myWorld, Vector2 archPos)
        {
            SetDirection(direction);
            switch (_direction)
            {
                case 1:
                    if(myWorld.isPosWalkable(GetPos().X, GetPos().Y - 1))
                        SetPos(GetPos().X, GetPos().Y - 1);
                    else
                        RemoveArrow(archPos);
                    break;
                case 2:
                    if (myWorld.isPosWalkable(GetPos().X, GetPos().Y + 1))
                        SetPos(GetPos().X, GetPos().Y + 1);
                    else
                        RemoveArrow(archPos);
                    break;
                case 3:
                    if (myWorld.isPosWalkable(GetPos().X - 1, GetPos().Y))
                        SetPos(GetPos().X - 1, GetPos().Y);
                    else
                        RemoveArrow(archPos);
                    break;
                case 4:
                    if (myWorld.isPosWalkable(GetPos().X + 1, GetPos().Y))
                        SetPos(GetPos().X + 1, GetPos().Y);
                    else
                        RemoveArrow(archPos);
                    break;
                default:
                    break;
            }
        }

        public void isVisible(bool visible) => 
            mark = !visible ? "" : "+";

        public void Draw()
        {
            ForegroundColor = color;
            SetCursorPosition((int)GetPos().X, (int)GetPos().Y);
            Write(mark);
            ResetColor();
        }

        public void UpdatePos(Vector2 archPos, World myWorld)
        {
            if (isFligh)
                rate++;
            if (rate <= distance)
                Fire(_direction, myWorld, archPos);
            if (rate == distance)
                RemoveArrow(archPos);
        }

        public void RemoveArrow(Vector2 archPos)
        {
            isVisible(false);
            SetPos(archPos);
            rate = distance;
        }

        public Vector2 GetPos() => position.Pos;

        public void SetPos(float Y, float X) =>
            position.Pos = new Vector2(Y, X);

        public void SetPos(int Y, int X) =>
            position.Pos = new Vector2(Y, X);

        public void SetPos(Vector2 Pos) =>
            position.Pos = Pos;
    }
}
