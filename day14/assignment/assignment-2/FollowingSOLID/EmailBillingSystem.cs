using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using assignment_2.FollowingSOLID.Models.Interfaces;

namespace assignment_2.FollowingSOLID
{
    internal class EmailBillingSystem : IBillingSystem
    {
        public void createBill(List<Ingredients> ingredients)
        {
            Console.WriteLine("\nThe bill has been emailed to the customer");
        }
    }
}
