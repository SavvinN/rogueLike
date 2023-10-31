namespace rogueLike
{
    public struct constants
    {
        public static String ground = " ", wall = "█", room = ".", player = "o", zombie = "Z";
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