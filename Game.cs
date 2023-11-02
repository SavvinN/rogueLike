using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace rogueLike
{
    internal class Game
    {

        private World myWorld;
        private Player currentPlayer;
        private Zombie[] zombie;
        private static int updateRate = 0;
        private static int level = 1;
        public static bool gameResult = false;


        public void Start()
        {
            Grid = new Maze((level * 2) + 18, (level * 15) + 5);
            currentPlayer = new Player(PlayerPosGenerator(Maze.Grid));
            zombie = new Zombie[level * 2];
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
            /*
            foreach(var z in zombie)
            {
                z.Draw();
            }
            */
            DrawGameStats();
        }

        private void HandlePlayerInput()
        {
            Vector2 playerPos = currentPlayer.GetPos();
            ConsoleKeyInfo keyInfo = ReadKey(true);
            ConsoleKey key = keyInfo.Key;
            switch(key)
            {
                case ConsoleKey.UpArrow:
                    if(myWorld.isPosWalkable(playerPos))
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

        private int PlayerPosGenerator(String[,] grid)
        {
            int Xmark = 0;
            int PosX = 0;
            Random rnd = new Random();
            for (int x = 0; x < grid.GetLength(1); x++)
            {
                if (grid[grid.GetLength(0) - 1, x] == "X")
                {
                    Xmark = x;
                    break;
                }
            }

            if (Xmark < grid.GetLength(1) / 2)
            {
                PosX = rnd.Next(grid.GetLength(1) / 2, grid.GetLength(1) - 1);
            }
            else PosX = rnd.Next(1, grid.GetLength(1) / 2);


            if (grid[1, PosX] != "█")
                return PosX;
            else
                return PosX + 1;
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
