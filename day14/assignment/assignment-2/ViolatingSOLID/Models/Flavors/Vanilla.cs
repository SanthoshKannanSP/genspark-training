using assignment_2.ViolatingSOLID.Models.Interfaces;

namespace assignment_2.ViolatingSOLID.Models.Flavors
{
    internal class Vanilla : Ingredient
    {
        public string Name => "Vanilla";

        public int Cost => 30;

        public void AddIcecream()
        {
            Console.WriteLine("Vanilla Icecream Added");
        }

        public void AddTopping()
        {
            Console.WriteLine("This is not topping");
        }
    }
}
