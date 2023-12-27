﻿using rogueLike.GameObjects.MazeObjects;
using System.Numerics;


namespace rogueLike.GameObjects
{
    internal class Arrow : GameObject
    {
        private Direction _direction;
        private int _distance = 5;
        GameObject[,] _grid;
        public Arrow(Vector2 pos, Direction direction, GameObject[,] grid)
        {
            Walkable = true;
            SetColor(ConsoleColor.Cyan);
            SetSymbol('+');
            SetPos(pos);
            _direction = direction;
            _grid = grid;
            Move();
        }

        public void Move()
        {
            var movingPos = Vector2.Zero;

            switch (_direction)
            {
                case Direction.Up:
                    movingPos = (Position - Vector2.UnitX);
                    break;
                case Direction.Down:
                    movingPos = (Position + Vector2.UnitX);
                    break;
                case Direction.Right:
                    movingPos = (Position + Vector2.UnitY);
                    break;
                case Direction.Left:
                    movingPos = (Position - Vector2.UnitY);
                    break;
            }

            if (_grid[(int)movingPos.X, (int)movingPos.Y].GetType() != new Wall().GetType() && !(_distance < 0))
            {
                SetPos(movingPos);
                _distance--;
            }
            else
                RemoveArrow();
        }

        public void RemoveArrow()
        {
            SetSymbol(new char());
            SetPos(Vector2.Zero);
        }
    }
}
