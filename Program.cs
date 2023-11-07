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
            Game currentGame = new Game();
            currentGame.Start();
        }
    }
}