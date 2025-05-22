using assignment_2.ViolatingSOLID.Models.Interfaces;

namespace assignment_2.ViolatingSOLID.Models.Toppings
{
    internal class Sprinkles : Ingredient
    {
        public string Name => "Sprinkles";

        public int Cost => 30;

        public void AddIcecream()
        {
            Console.WriteLine("This is not icecream");
        }

        public void AddTopping()
        {
            Console.WriteLine("Sprinkles Topping Added");
        }
    }
}
