using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace rogueLike
{
    internal class Zombie : Enemy
    {
        //Убивает игрока когда касается его
        public Zombie() : base()
        {
            SetColor(ConsoleColor.Red);
            SetMarker("Z");
        }
    }
}
