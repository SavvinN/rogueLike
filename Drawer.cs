using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace rogueLike
{
    internal class Drawer
    {
        public static void DrawFrame(World myWorld, Player currentPlayer, Zombie[] zombie)
        {
            myWorld.Draw();
            currentPlayer.Draw();
            foreach(var z in zombie)
            {
                z.Draw();
            } 
        }

        public static void DrawGameStats(int Y, int updateRate, int level)
        {
            SetCursorPosition(0, Y + 1);
            Write($"UpdateRate: {updateRate} Уровень: {level}");
        }

        public static void DrawIntro()
        {
            Clear();
            WriteLine("Hi, press any key to start ");
            ReadKey();
            Clear();
        }

        public static void DrawOutroWin()
        {
            Clear();
            WriteLine("u win\npress any key to go to another level");
            ReadKey();
            Clear();
        }

        public static void DrawOutroLose()
        {
            Clear();
            WriteLine("U lose\npress key to restart");
            ReadKey();
            Clear();
        }
    }
}
