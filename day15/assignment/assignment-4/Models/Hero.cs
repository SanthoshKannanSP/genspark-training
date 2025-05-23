using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using assignment_4.Interfaces;

namespace assignment_4.Models
{
    internal class Hero
    {
        public string Name { get; init; }
        public int Strength { get; init; }
        public IWeapon Weapon { get; init; }

        public int Attack(Goblin goblin)
        {
            var damageDealt = Weapon.DealDamage(Strength, goblin.Defence);
            Console.WriteLine($"{Name} dealt {damageDealt} to Goblin");
            return damageDealt;
        }
    }
}
