﻿using rogueLike.GameObjects.MazeObjects;
using System.Numerics;

namespace rogueLike.GameObjects.Enemys
{

    internal class Enemy : Creature
    {
        private int viewDistance = 6;
        public List<Vector2> path = new List<Vector2>();

        public Enemy(Vector2 spawnPos)
        {
            Walkable = false;
        }

        protected void SetViewDistance(int dist) => viewDistance = dist;

        public void Spawn(Vector2 spawnPos) => SetPos(spawnPos);

        public void Patrol(GameObject[,] grid)
        {

        }

        public void BackToRoom(GameObject[,] grid)
        {

        }

        public bool FindPlayer(Vector2 playerPosition, GameObject[,] grid)
        {
            if (Vector2.Distance(GetPos(), playerPosition) < (float)viewDistance)
            {
                List<Vector2> seePath = GetPathTo(playerPosition);

                foreach (var pos in seePath)
                {
                    if (World.CompareObjects(grid[(int)pos.X, (int)pos.Y], new Wall()))
                        return false;
                }
                path = GetPathTo(playerPosition);
                return true;
            }
            return false;
        }

        private List<Vector2> GetPathTo(Vector2 playerPosition)
        {
            List<Vector2> path = new List<Vector2>();
            Vector2 viewPoint = GetPos();
            path.Add(viewPoint);
            while (viewPoint != playerPosition)
            {
                float temp = viewPoint.X;
                    viewPoint.X += (viewPoint.X > playerPosition.X)
                                    ? -1
                                    : (viewPoint.X < playerPosition.X)
                                        ? 1
                                        : 0;

                if (temp == viewPoint.X)
                    viewPoint.Y += (viewPoint.Y > playerPosition.Y)
                                    ? -1
                                    : (viewPoint.Y < playerPosition.Y)
                                        ? 1
                                        : 0;
                path.Add(viewPoint);
            }
            return path;
        }

        public Vector2 GetNextStep(Vector2 playerPosition)
        {
            if (path.Count > 1)
            {
                return path[1];
            }
            else
                return Position;
        }

    }

}
