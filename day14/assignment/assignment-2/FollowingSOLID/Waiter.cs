using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment_2.FollowingSOLID
{
    internal class Waiter
    {
        public Order TakeOrder()
        {
            Order order = new Order();
            order.flavor = OrderFlavor();
            order.topping = OrderTopping();
            return order;
        }

        private static Toppings OrderTopping()
        {
            Console.WriteLine("Please select topping: ");
            foreach (Toppings toppingName in Enum.GetValues(typeof(Toppings)))
            {
                Console.WriteLine($"{(int)toppingName}. {toppingName}");
            }
            var userChoice = Console.ReadLine().Trim();

            int toppings;
            while (!int.TryParse(userChoice, out toppings) || (toppings < 1 || toppings > 3))
            {
                Console.WriteLine("Enter a valid choice");
                userChoice = Console.ReadLine().Trim();
            }
            return (Toppings)toppings;
        }

        private static Flavors OrderFlavor()
        {
            Console.WriteLine("Please choose icecream flavor: ");
            foreach (Flavors flavorName in Enum.GetValues(typeof(Flavors)))
            {
                Console.WriteLine($"{(int)flavorName}. {flavorName}");
            }
            var userChoice = Console.ReadLine().Trim();
            int flavor;
            while (!int.TryParse(userChoice, out flavor) || (flavor < 1 || flavor > 3))
            {
                Console.WriteLine("Enter a valid choice");
                userChoice = Console.ReadLine().Trim();
            }
            return (Flavors)flavor;
        }
    }
}
