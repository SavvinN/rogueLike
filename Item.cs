using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace rogueLike
{
    internal interface Item
    {
        public void PickUp(Vector2 playerPos);

        public void Action();

        public void Draw();
    }
}
