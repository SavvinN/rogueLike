

namespace rogueLike.GameObjects.MazeObjects
{
    internal class Wall : GameObject
    {
        private const char symbol = '█';
        public Wall()
        {
            Walkable = false;
            SetColor(ConsoleColor.White);
            SetSymbol(symbol);
        }
    }
}
