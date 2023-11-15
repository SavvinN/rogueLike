using System.Numerics;

namespace rogueLike.GameObjects.Enemys
{
    internal class Archer : Enemy
    {
        private bool isShoot = false;
        private int fireCooldown = 60;

        public Archer(Vector2 spawnPos) : base(spawnPos)
        {
            SetPos(spawnPos);
            SetColor(ConsoleColor.Blue);
            SetSymbol('A');
            SetViewDistance(8);
        }

        private Direction GetPlayerDirection(Vector2 PlayerPos)
        {
            Direction playerDirection;
            if(GetPos() == PlayerPos)
            {
                playerDirection = Direction.None;
            }
            else
            {
                playerDirection = (GetPos().X < PlayerPos.X) ?
                                  Direction.Right
                                  : Direction.Left;

                playerDirection = (GetPos().Y > PlayerPos.Y) ?
                                  Direction.Down
                                  : Direction.Up;
            }
            return playerDirection;
        }

        private void Escape(Direction playerDirection, GameObject[,] grid)
        {

        }
    }
}
