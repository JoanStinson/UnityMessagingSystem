namespace JGM.MessagingSystem
{
    public readonly struct PlayerSpawnMessage
    {
        public readonly int Health;
        public readonly float Speed;

        public PlayerSpawnMessage(int health, float speed)
        {
            Health = health;
            Speed = speed;
        }

        public override string ToString()
        {
            return $"Health: {Health} Speed: {Speed}";
        }
    }
}