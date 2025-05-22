using assignment_2.ViolatingSOLID.Models.Interfaces;

namespace assignment_2.ViolatingSOLID.Models.Flavors
{
    internal class Chocolate : Ingredient
    {
        public string Name => "Chocolate";

        public int Cost => 30;

        public void AddIcecream()
        {
            Console.WriteLine("Chocolate Icecream Added");
        }

        public void AddTopping()
        {
            Console.WriteLine("This is not topping");
        }
    }
}
