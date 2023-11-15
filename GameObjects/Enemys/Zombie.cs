
using System.Numerics;

namespace rogueLike.GameObjects.Enemys
{
    internal class Zombie : Enemy
    {
        //Убивает игрока когда касается его
        public Zombie(Vector2 spawnPos) : base(spawnPos)
        {
            SetColor(ConsoleColor.Red);
            SetSymbol('Z');
            SetPos(spawnPos);
            SetViewDistance(6);
        }

    }
}
