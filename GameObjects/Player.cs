using System.Numerics;

namespace rogueLike.GameObjects
{
    internal class Player : Creature
    {
        private const char mainSymbol = 'o';
        public Player(Vector2 pos)
        {
            maxStamina = 120;
            Walkable = false;
            SetPos(pos);
            SetSymbol(mainSymbol);
            SetColor(ConsoleColor.Green);
        }
    }
}
