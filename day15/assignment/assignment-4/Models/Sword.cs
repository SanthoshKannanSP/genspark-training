using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using assignment_4.Interfaces;

namespace assignment_4.Models
{
    internal class Sword : IWeapon
    {
        public string Name => "Sword";

        public int damage => 10;

        public int DealDamage(int heroStrength, int EnemyDefence)
        {
            return damage + heroStrength - EnemyDefence;
        }
    }
}
