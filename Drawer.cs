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
        public static void DrawFrame(World myWorld, Player currentPlayer, Zombie[] zombie, Archer[] archer)
        {
            myWorld.Draw();
            currentPlayer.Draw();
            foreach (var a in archer)
            {
                a.Draw();
                a.DrawArrow();
            }
            foreach (var z in zombie)
            {
                z.Draw();
            }
        }

        public static void DrawItems(Key key, Heart heart)
        {
            key.Draw();
            heart.Draw();
        }

        public static void DrawGameStats(int Y, int updateRate, int level, int life)
        {
            SetCursorPosition(0, Y + 1);
            Write($"UpdateRate: {updateRate} LVL: {level} Life: {life}");
        }

        public static void DrawIntro()
        {
            Clear();
            WriteLine("Нажмите на любую кнопку чтобы начать играть!\n\n" +
                "Двигаться на стрелках\n" +
                "Атака производится путем нажатия кнопок WASD\n\n" +
                "Цель: найти ключ, и выйти из лабиринта\n" +
                "(ключ выпадает после смерти моба с шансом 10%, или гарантированно после убийств последнего моба)");
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
            WriteLine("U lose");
            ReadKey();
            Clear();
        }
    }
}
