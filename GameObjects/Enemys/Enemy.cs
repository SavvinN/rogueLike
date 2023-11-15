using rogueLike.GameObjects.MazeObjects;
using System.Numerics;

namespace rogueLike.GameObjects.Enemys
{

    internal class Enemy : Creature
    {
        private int viewDistance = 6;
        protected List<Vector2> path = new List<Vector2>();
        private bool isChasing = false;
        protected bool isDead = false;

        public Enemy(Vector2 spawnPos)
        {
            Walkable = false;
        }

        protected void SetViewDistance(int dist) => viewDistance = dist;

        public void Spawn(Vector2 spawnPos) => SetPos(spawnPos);

        public void MoveToPlayer(Vector2 playerPos) => SetPos(new Vector2(path[1].X, path[1].Y));

        public void ChasePlayer(Vector2 playerPos, GameObject[,] grid)
        {

        }

        public void Patrol(GameObject[,] grid)
        {

        }
        public bool SeePlayer(Vector2 playerPosition, GameObject[,] grid)
        {
            if (Vector2.Distance(GetPos(), playerPosition) < viewDistance)
            {
                Vector2 viewPoint = GetPos();
                while (viewPoint != playerPosition)
                {
                    viewPoint.X += (viewPoint.X > playerPosition.X)
                                    ? -1
                                    : (viewPoint.X < playerPosition.X)
                                        ? 1
                                        : 0;
                    viewPoint.Y += (viewPoint.Y > playerPosition.Y)
                                    ? 1
                                    : (viewPoint.Y < playerPosition.Y)
                                        ? -1
                                        : 0;
                    if (grid[(int)viewPoint.Y, (int)viewPoint.X] == new Wall())
                        return true;
                }
            }
            return false;
        }

        public void Dead()
        {

        }

    }

}
