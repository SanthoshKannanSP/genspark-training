using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment_2.FollowingSOLID.Models.Interfaces
{
    internal interface Ingredients
    {
        public string Name { get; }
        public int Cost { get; }

        public void Make();
    }
}
