using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace rogueLike
{
    internal class Archer : Enemy
    {

        // Убивает когда попадает стрелой по игроку, но умирает если игрок консется
        public Archer(Vector2 spawnPos) : base(spawnPos)
        {
            SetColor(ConsoleColor.Blue);
            SetMarker("A");
        }

    }
}
