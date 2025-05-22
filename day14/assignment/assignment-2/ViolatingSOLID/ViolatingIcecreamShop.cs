using assignment_2.ViolatingSOLID.Models.Flavors;
using assignment_2.ViolatingSOLID.Models.Interfaces;
using assignment_2.ViolatingSOLID.Models.Toppings;


namespace assignment_2.ViolatingSOLID
{
    internal class ViolatingIcecreamShop
    {
        private readonly Billing _billing;
        public ViolatingIcecreamShop() 
        {
            _billing = new Billing();
        }
        public void ServiceCustomer()
        {
            GetCustomerOrder(out Flavors flavor, out Toppings topping);
            var icecream = GetFlavor(flavor);
            var toppings = GetTopping(topping);
            MakeOrder(icecream, toppings);
            _billing.DisplayBill(icecream, toppings);
        }
        private void GetCustomerOrder(out Flavors flavor, out Toppings topping)
        {
            flavor = (Flavors)OrderFlavor();
            topping = (Toppings)OrderTopping();
        }

        private static int OrderFlavor()
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
            return flavor;
        }

        private static int OrderTopping()
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
            return toppings;
        }

        private Ingredient GetFlavor(Flavors flavor)
        {
            Ingredient icecream = null;
            switch (flavor)
            {
                case Flavors.Vanilla:
                    icecream = new Vanilla();
                    break;
                case Flavors.Strawberry:
                    icecream = new Strawberry();
                    break;
                case Flavors.Chocolate:
                    icecream = new Chocolate();
                    break;
            }
            return icecream;
        }

        private Ingredient GetTopping(Toppings toppings)
        {
            Ingredient topping = null;
            switch (toppings)
            {
                case Toppings.Almonds:
                    topping = new Almonds();
                    break;
                case Toppings.Sprinkles:
                    topping = new Sprinkles();
                    break;
                case Toppings.Oreos:
                    topping = new Oreos();
                    break;
            }
            return topping;
        }

        private void MakeOrder(Ingredient icecream, Ingredient toppings)
        {
            Console.WriteLine("\nMaking Order");
            icecream.AddIcecream();
            toppings.AddTopping();
        }
    }
}
