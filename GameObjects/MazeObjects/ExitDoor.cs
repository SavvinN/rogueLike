﻿
namespace rogueLike.GameObjects.MazeObjects
{
    internal class ExitDoor : GameObject
    {
        private const char symbol = 'X';
        public ExitDoor()
        {
            Walkable = true;
            SetColor(ConsoleColor.White);
            SetSymbol(symbol);
        }
    }
}
