using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rogueLike
{
    internal class Archer : Enemy
    {
        // Убивает когда попадает стрелой по игроку, но умирает если игрок консется
        public Archer() : base()
        {
            SetColor(ConsoleColor.DarkRed);
            SetMarker("A");
        }
    }
}
