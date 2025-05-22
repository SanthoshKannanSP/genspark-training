using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using assignment_2.ViolatingSOLID.Models.Interfaces;

namespace assignment_2.ViolatingSOLID
{
    internal class Billing
    {
        public void DisplayBill(Ingredient icecream, Ingredient toppings)
        {
            Console.WriteLine("\nBill");
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine($"{icecream.Name} Icecream: {icecream.Cost}");
            Console.WriteLine($"{toppings.Name} Topping: {toppings.Cost}");
            Console.WriteLine($"Total Amount: {icecream.Cost + toppings.Cost}");
            Console.WriteLine("------------------------------------------------");
        }

        public void EmailBill(Ingredient icecream, Ingredient toppings)
        {
            Console.WriteLine("The bill has been emailed to the customer");
        }
    }
}
