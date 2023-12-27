
using System.Numerics;

namespace rogueLike.GameObjects.Enemys
{
    internal class Zombie : Enemy
    {
        public Zombie(Vector2 spawnPos) : base(spawnPos)
        {
            SetColor(ConsoleColor.Red);
            SetSymbol('Z');
            SetPos(spawnPos);
            SetViewDistance(6);
        }

    }
}
