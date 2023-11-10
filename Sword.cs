using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace rogueLike
{
    internal class Sword : Item
    {
        private Position _position;
        private bool isPickUp = false;
        private String Marker;
        private ConsoleColor Color = ConsoleColor.Cyan;
        private int cooldownTime = 30;

        public Sword()
        {
            Marker = "";
        }
        public void Action()
        {
            isPickUp = true;
        }

        public void Draw()
        {
            ForegroundColor = Color;
            SetCursorPosition((int)_position.Pos.Y, (int)_position.Pos.X);
            Write(Marker);
            ResetColor();
        }

        public void PickUp(Vector2 playerPos)
        {
            if (playerPos == _position.Pos)
            {
                Action();
                _position.Pos = new Vector2(0, 0);
                Marker = "";
            }
        }

        public void SpawnSword(List<Vector2> map)
        {
            if (!isPickUp)
            {
                Random rnd = new Random();
                Marker = "!";
                _position.Pos = map[rnd.Next(map.Count - 1)];
            }
        }

        public int GetCooldownTime() => cooldownTime;
        public bool GetExist() => isPickUp;
        public void SetExist(bool pickUp) => isPickUp = pickUp;
    }
}
