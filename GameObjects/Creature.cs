using System.Numerics;

namespace rogueLike.GameObjects
{
    internal class Creature : GameObject
    {
        protected int maxStamina = 120;
        protected int attackCost = 50;
        protected int moveCost = 20;
        public int currentStamina = 0;
        public bool isReadyToMove = false;
        public Creature()
        {
            SetSymbol('?');
            SetColor(ConsoleColor.Red);
            Walkable = true;
        }
        public void Move(Direction direct)
        {
            if (currentStamina > moveCost)
            {
                Vector2 movedPos = Vector2.Zero;
                switch (direct)
                {
                    case Direction.Up:
                        movedPos = new Vector2(GetPos().X - 1, GetPos().Y);
                        break;
                    case Direction.Down:
                        movedPos = new Vector2(GetPos().X + 1, GetPos().Y);
                        break;
                    case Direction.Left:
                        movedPos = new Vector2(GetPos().X, GetPos().Y - 1);
                        break;
                    case Direction.Right:
                        movedPos = new Vector2(GetPos().X, GetPos().Y + 1);
                        break;
                }
                SetPos(movedPos);
                isReadyToMove = false;
                currentStamina -= moveCost;
            }
        }

        public virtual Vector2 Attack(Direction direct)
        {
            if (currentStamina > attackCost)
            {
                switch (direct)
                {
                    case Direction.Up:
                        currentStamina -= attackCost;
                        return new Vector2(GetPos().X - 1, GetPos().Y);
                    case Direction.Down:
                        currentStamina -= attackCost;
                        return new Vector2(GetPos().X + 1, GetPos().Y);
                    case Direction.Left:
                        currentStamina -= attackCost;
                        return new Vector2(GetPos().X, GetPos().Y - 1);
                    case Direction.Right:
                        currentStamina -= attackCost;
                        return new Vector2(GetPos().X, GetPos().Y + 1);
                }
            }
            return Vector2.Zero;
        }

        public void RecoverStamina() => currentStamina = (currentStamina < maxStamina)
                                                          ? currentStamina + 1
                                                          : maxStamina;
    }
}
