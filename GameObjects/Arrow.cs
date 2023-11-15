using System.Numerics;


namespace rogueLike.GameObjects
{
    internal class Arrow : GameObject
    {
        private int _direction, 
                    _distance = 8;
        private bool _isFligh = false;
        public Arrow(Vector2 pos, int direction)
        {
            Walkable = true;
            SetColor(ConsoleColor.Cyan);
            SetSymbol('+');
            SetPos(pos);
            SetDirection(direction);
        }

        public void SetDirection(int direction)
        {
            _direction = direction;
        }

        public void Move(int direction, GameObject[,] grid)
        {
           int x = (int)GetPos().X, 
               y = (int)GetPos().Y;
           switch(direction)
            {
                case (int)Direction.Up:
                    if (grid[y - 1,x].IsWalkable)
                        SetPos(new Vector2(y - 1, x));
                    break;
                case (int)Direction.Down:
                    if (grid[y + 1, x].IsWalkable)
                        SetPos(new Vector2(y + 1, x));
                    break;
                case (int)Direction.Left:
                    if (grid[y, x - 1].IsWalkable)
                        SetPos(new Vector2(y, x - 1));
                    break;
                case (int)Direction.Right:
                    if (grid[y, x + 1].IsWalkable)
                        SetPos(new Vector2(y, x + 1));
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public void RemoveArrow(int distance)
        {
            if(distance == _distance)
            {

            }
        }
    }
}
