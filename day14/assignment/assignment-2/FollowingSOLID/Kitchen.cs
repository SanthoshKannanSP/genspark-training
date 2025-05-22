using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using assignment_2.FollowingSOLID.Models.Interfaces;
namespace assignment_2.FollowingSOLID
{
    internal class Kitchen
    {
        public void MakeOrder(List<Ingredients> ingredients)
        {
            Console.WriteLine("\nMaking Order");
            foreach (var ingredient in ingredients)
                ingredient.Make();
        }
    }
}
