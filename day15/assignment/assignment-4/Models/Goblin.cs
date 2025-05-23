namespace assignment_4.Models
{
    public class Goblin
    {
        private int _health = 100;
        public int Defence { get; init; }

        public void TakeDamage(int damageDealt)
        {
            _health -= damageDealt;
            Console.WriteLine($"Goblin health is {_health}");
        }
    }
}