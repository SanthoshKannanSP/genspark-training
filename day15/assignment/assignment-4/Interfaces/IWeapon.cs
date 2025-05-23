namespace assignment_4.Interfaces
{
    internal interface IWeapon
    {
        string Name { get; }
        int damage { get; }

        public int DealDamage(int heroStrength, int EnemyDefence);
    }
}