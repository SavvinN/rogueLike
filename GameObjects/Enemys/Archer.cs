﻿using System.Numerics;

namespace rogueLike.GameObjects.Enemys
{
    internal class Archer : Enemy
    {
        public Archer(Vector2 spawnPos) : base(spawnPos)
        {
            attackCooldown = 50;
            SetPos(spawnPos);
            SetColor(ConsoleColor.Blue);
            SetSymbol('A');
            SetViewDistance(8);
        }

        private Direction GetPlayerDirection(Vector2 PlayerPos)
        {
            Direction playerDirection = Direction.None;
            if(PlayerPos.X == Position.X)
            {
                if (PlayerPos.Y < Position.Y)
                {
                    playerDirection = Direction.Down;
                }
                else if (PlayerPos.Y > Position.Y)
                {
                    playerDirection = Direction.Up;
                }
            }
            else if(PlayerPos.Y == Position.Y)
            {
                if (PlayerPos.X < Position.X)
                {
                    playerDirection = Direction.Left;
                }
                else if (PlayerPos.X > Position.X)
                {
                    playerDirection = Direction.Right;
                }
            }
            return playerDirection;
        }

    }
}
