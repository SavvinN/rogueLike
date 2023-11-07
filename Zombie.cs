using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace rogueLike
{
    internal class Zombie : Enemy
    {
        //Убивает игрока когда касается его
        public Zombie(Vector2 spawnPos) : base(spawnPos)
        {
            SetColor(ConsoleColor.Red);
            SetMarker("Z");
        }

        public override void MoveToPlayer(Vector2 playerPos, World myWorld)
        {
             SetPos(path[1].X, path[1].Y);
        }

    }
}
