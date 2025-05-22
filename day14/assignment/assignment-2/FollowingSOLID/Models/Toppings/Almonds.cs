using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment_2.FollowingSOLID.Models.Toppings
{
    internal class Almonds : Topping
    {
        public override string Name => "Almonds";

        public override int Cost => 30;

        protected override void MakeTopping()
        {
            Console.WriteLine("Almonds Topping Added");
        }
    }
}
