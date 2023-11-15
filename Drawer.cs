
using System.Numerics;
using System.Timers;
using rogueLike.GameObjects;
using rogueLike.GameObjects.Enemys;
using rogueLike.GameObjects.Items;
using static System.Console;

namespace rogueLike
{
    internal class Drawer
    {
        private static void DrawObject(GameObject gameObject)
        {
            ForegroundColor = gameObject.GetColor();
            Vector2 pos = gameObject.GetPos();
            SetCursorPosition((int)pos.Y, (int)pos.X);
            Write(gameObject.GetSymbol());
            ResetColor();
        }
        public static void DrawFrame(String frame, Player currentPlayer, Zombie[] zombie, Archer[] archer)
        {
            SetCursorPosition(0, 0);
            Write(frame);
            foreach(var z in zombie)
            {
                DrawObject(z);
            }
            foreach (var a in archer)
            {
                DrawObject(a);
            }
            DrawObject(currentPlayer);
        }

        public static void DrawGameStats(int X, int frameCount, int stamina)
        {
            SetCursorPosition(X, 0);
            WriteLine($"Frame: {frameCount}    ");

            string staminaIndicator = "";
            for(int i = 0; i < stamina / 10; i++)
            {
                staminaIndicator += "|";
            }
            SetCursorPosition(X, 1);
            WriteLine($"stamina: {staminaIndicator}                  ");
        }

        public static void DrawAttack(Vector2 pos)
        {
            SetCursorPosition((int)pos.Y, (int)pos.X);
            Write('*');
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
