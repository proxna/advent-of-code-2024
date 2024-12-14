namespace AdventOfCode.Day14
{
    internal class Robot
    {
        public (int x, int y) Position { get; set; }

        public (int x, int y) Velocity { get; set; }

        public Robot((int x, int y) position, (int x, int y) velocity)
        {
            Position = position;
            Velocity = velocity;
        }

        public void Move((int x, int y) mapSize)
        {
            Position = (Position.x + Velocity.x, Position.y + Velocity.y);
            TeleportIfNecessary(mapSize);
        }

        private void TeleportIfNecessary((int x, int y) mapSize)
        {
            int newPosX = Position.x;
            int newPosY = Position.y;

            if (Position.x < 0)
            {
                newPosX = mapSize.x - (-Position.x);
            }

            if(Position.y < 0)
            {
                newPosY = mapSize.y - (-Position.y);
            }

            if(Position.x >= mapSize.x)
            {
                newPosX = Position.x - mapSize.x;
            }

            if (Position.y >= mapSize.y)
            {
                newPosY = Position.y - mapSize.y;
            }

            Position = (newPosX, newPosY);
        }
    }
}
