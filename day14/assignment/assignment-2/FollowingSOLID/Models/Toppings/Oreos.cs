using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment_2.FollowingSOLID.Models.Toppings
{
    internal class Oreos : Topping
    {
        public override string Name => "Oreos";

        public override int Cost => 30;

        protected override void MakeTopping()
        {
            Console.WriteLine("Oreos Topping Added");
        }
    }
}
