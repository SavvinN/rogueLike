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

        public static void DrawItems(Key key, Heart heart, Sword sword)
        {
            key.Draw();
            heart.Draw();
            sword.Draw();
        }

        public static void DrawGameStats(int X, int updateRate, int level, int life, int cooldown, bool key, bool sword)
        {
            SetCursorPosition(X, 0);
            string k;
            if(key)
            {
                k = "найден";
            }
            else
            {
                k = "не найден";
            }
            string s;
            if (sword)
            {
                s = "подобран";
            }
            else
            {
                s = "не подобран";
            }
            WriteLine($"UpdateRate: {updateRate}    ");
            SetCursorPosition(X, 1);
            WriteLine($"LVL: {level} Life: {life}    ");
            SetCursorPosition(X, 2);
            WriteLine($"Attack cooldown: {cooldown}  ");
            SetCursorPosition(X, 3);
            WriteLine($"Ключ: {k}      ");
            SetCursorPosition(X, 4);
            WriteLine($"Меч: {s}      ");
        }

        public static void DrawIntro()
        {
            Clear();
            WriteLine("Нажмите на любую кнопку чтобы начать играть!\n\n" +
                "Двигаться можно на стрелках\n" +
                "Атака производится путем нажатия кнопок WASD\n\n" +
                "Цель: найти ключ, и выйти из лабиринта\n" +
                "Подсказки:\n" +
                "Ключ выпадает после смерти моба с шансом 10%, или гарантированно после убийств последнего моба\n" +
                "Меч может заблокировать стрелу, также меч уменьшает кд удара");
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
