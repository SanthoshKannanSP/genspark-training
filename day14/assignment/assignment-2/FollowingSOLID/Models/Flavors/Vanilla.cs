using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment_2.FollowingSOLID.Models.Flavors
{
    internal class Vanilla : Icecream
    {
        public override string Name => "Vanilla";

        public override int Cost => 30;

        public override void MakeIcecream()
        {
            Console.WriteLine("Vanilla Icecream Added");
        }
    }
}
