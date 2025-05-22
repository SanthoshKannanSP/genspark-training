using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using assignment_2.FollowingSOLID.Models.Interfaces;

namespace assignment_2.FollowingSOLID.Models.Flavors
{
    internal abstract class Icecream : Ingredients
    {
        public abstract string Name { get; }

        public abstract int Cost { get; }

        public void Make()
        {
            MakeIcecream();
        }

        public abstract void MakeIcecream();
    }
}
