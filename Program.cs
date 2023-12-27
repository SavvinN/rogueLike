﻿
namespace rogueLike
{
    enum Direction : byte
    {
        Up = 0,
        Left = 1,
        Right = 2,
        Down = 3,
        None = 4,
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Game currentGame = new Game();
            currentGame.Start();
        }
    }
}