using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment_2.FollowingSOLID.Models.Flavors
{
    internal class Chocolate : Icecream
    {
        public override string Name => "Chocolate";

        public override int Cost => 30;

        public override void MakeIcecream()
        {
            Console.WriteLine("Chocolate Icecream Added");
        }
    }
}
