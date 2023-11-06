using System.Numerics;

namespace rogueLike
{
    struct Position()
    {
        public Vector2 Pos { get; set; }
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