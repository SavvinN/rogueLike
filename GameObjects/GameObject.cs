using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace rogueLike.GameObjects
{
    internal abstract class GameObject
    {
        private char _symbol;
        private ConsoleColor _color;
        private Vector2 _position;
        protected bool Walkable { get; set; }
        public bool IsWalkable { get => Walkable; }
        public Vector2 GetPos() => _position;
        public void SetPos(Vector2 pos) => _position = pos;
        public char GetSymbol() => _symbol;
        protected void SetSymbol(char chr) => _symbol = chr;
        protected void SetColor(ConsoleColor color) => _color = color;
        public ConsoleColor GetColor() => _color;
    }
}
