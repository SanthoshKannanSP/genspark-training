using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using assignment_2.FollowingSOLID.Models.Interfaces;

namespace assignment_2.FollowingSOLID.Models.Toppings
{
    internal abstract class Topping : Ingredients
    {
        public abstract string Name { get; }

        public abstract int Cost { get; }

        public void Make()
        {
            MakeTopping();
        }

        protected abstract void MakeTopping();
    }
}
