using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
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
        private Zombie[] zombie;
        private static int updateRate = 0;
        private static int level = 1;
        public static bool gameResult = false;


        public void Start()
        {
            maze = new Maze((level * 15) + 30, (level * 2) + 20);
            currentPlayer = new Player();
            zombie = new Zombie[level * 2];
            for(int i = 0; i < level * 2; i++) 
            {
                zombie[i] = new Zombie();
            }
            
            myWorld = new World();

            if (level == 1)
            intro();

            Clear();
            Loop();
            if(!gameResult) OutroWin(); else OutroLose();
        }

        private void DrawFrame()
        {
            myWorld.Draw();
            currentPlayer.Draw();
            foreach(var z in zombie)
            {
                z.Draw();
            }
            DrawGameStats();
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
                System.Threading.Thread.Sleep(2);
            }
        }

        private void DrawGameStats()
        {
            SetCursorPosition(0, Maze.Grid.GetLength(0));
            Write($"Ход: {updateRate} Уровень: {level}");
        }

        private void LevelUp()
        {
            level++;
            
        }

        private void intro()
        {
            Clear();
            WriteLine("Hi, press any key to start ");
            ReadKey();
        }

        private void OutroWin()
        {
            Clear();
            LevelUp();
            WriteLine("u win\npress any key to go to another level");
            ReadKey();
            Start();
        }

        private void OutroLose()
        {
            Clear();
            WriteLine("U lose\npress key to restart");
            ReadKey();
            Start();
        }

    }
}
