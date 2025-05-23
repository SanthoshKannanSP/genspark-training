using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using assignment_4.Interfaces;

namespace assignment_4
{
    internal class WeaponFactory
    {
        public Dictionary<Weapons, IWeapon> _weaponMap = new();

        public void AddWeapon(Weapons weaponName, IWeapon weaponClass)
        {
            _weaponMap[weaponName] = weaponClass;
        }

        public IWeapon GetWeapon(Weapons weaponName)
        {
            return _weaponMap[weaponName];
        }
    }
}
