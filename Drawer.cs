
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
        public static void DrawFrame(String frame,
                                     Player currentPlayer,
                                     Zombie[] zombie,
                                     Archer[] archer,
                                     List<Arrow> arrow)
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
            if(arrow != null)
            foreach (var a in arrow)
            {
                DrawObject(a);
            }
            DrawObject(currentPlayer);
        }

        public static void DrawGameStats(int X, int frameCount)
        {
            SetCursorPosition(X, 0);
            WriteLine($"Frame: {frameCount}    ");
        }

        public static void DrawAttack(Vector2 pos, Char ch)
        {
            SetCursorPosition((int)pos.Y, (int)pos.X);
            Write(ch);
        }

        public static void DrawIntro()
        {
            Clear();
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
