using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using assignment_2.FollowingSOLID.Models.Interfaces;

namespace assignment_2.FollowingSOLID
{
    internal class PhysicalBillingSystem : IBillingSystem
    {
        public void createBill(List<Ingredients> ingredients)
        {
            int totalCost = 0;
            Console.WriteLine("\nBill");
            Console.WriteLine("------------------------------------------------");
            foreach (var ingredient in ingredients)
            {
                Console.WriteLine($"{ingredient.Name} Icecream: {ingredient.Cost}");
                totalCost += ingredient.Cost;
            }
            Console.WriteLine($"Total Amount: {totalCost}");
            Console.WriteLine("------------------------------------------------");
        }
    }
}
