using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace rogueLike
{
    internal class Game
    {

        private World myWorld;
        private Maze maze;
        private Player currentPlayer;
        private Zombie zombie;
        private static int updateRate = 0;
        public static bool gameResult = false;

        public Game() 
        {
            maze = new Maze();
            currentPlayer = new Player();
            zombie = new Zombie();
            myWorld = new World();
        }

        public void Start()
        {
            maze = new Maze();
            currentPlayer = new Player();
            zombie = new Zombie();
            myWorld = new World();
            intro();
            Clear();
            Loop();
            if(!gameResult) OutroWin(); else OutroLose();
        }

        private void DrawFrame()
        {
            myWorld.Draw();
            DrawGameStats();
            zombie.Draw();
            currentPlayer.Draw();
        }

        private void HandlePlayerInput()
        {
            ConsoleKeyInfo keyInfo = ReadKey(true);
            ConsoleKey key = keyInfo.Key;
            switch(key)
            {
                case ConsoleKey.UpArrow:
                    if(myWorld.isPosWalkable(Player.X, Player.Y - 1))
                        Player.Y -= 1;
                    break;
                case ConsoleKey.DownArrow:
                    if (myWorld.isPosWalkable(Player.X, Player.Y + 1))
                        Player.Y += 1;
                    break;
                case ConsoleKey.RightArrow:
                    if (myWorld.isPosWalkable(Player.X + 1, Player.Y))
                        Player.X += 1;
                    break;
                case ConsoleKey.LeftArrow:
                    if (myWorld.isPosWalkable(Player.X - 1, Player.Y))
                        Player.X -= 1;
                    break;
                default:
                    break;
            }
        }
        private void Loop()
        {
            Console.CursorVisible = false;
            string elementAtPlayerPos;
            while (true)
            {
                DrawFrame();   
                updateRate++;
                elementAtPlayerPos = myWorld.GetElementAt(Player.X, Player.Y);
                if(elementAtPlayerPos == "X" || Game.gameResult)
                {
                    break;
                }
                HandlePlayerInput();
                System.Threading.Thread.Sleep(10);
            }
        }

        private void intro()
        {
            Clear();
            WriteLine("Hi, press any key to start");
            ReadKey();
        }

        private void OutroWin()
        {
            Clear();
            WriteLine("Congrats, u win\n Press any key to restart");
            ReadKey();
            Start();
        }

        private void OutroLose()
        {
            Clear();
            WriteLine("U lose\n press key to restart");
            ReadKey();
            Start();
        }

        private void DrawGameStats()
        {
            SetCursorPosition(0, 0);
            Write(updateRate);
        }

    }
}
