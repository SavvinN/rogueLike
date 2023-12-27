﻿using System.Numerics;

namespace rogueLike.GameObjects
{
    internal class Player : Creature
    {
        private const char mainSymbol = 'o';
     
        public Player(Vector2 pos)
        {
            moveCooldown = base.moveCooldown / 2;
            attackCooldown = base.attackCooldown / 2;

            Walkable = false;
            SetPos(pos);
            SetSymbol(mainSymbol);
            SetColor(ConsoleColor.Green);
        }
    }
}
