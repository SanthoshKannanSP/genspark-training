using assignment_2.ViolatingSOLID.Models.Interfaces;

namespace assignment_2.ViolatingSOLID.Models.Flavors
{
    internal class Strawberry : Ingredient
    {
        public string Name => "Strawberry";

        public int Cost => 30;

        public void AddIcecream()
        {
            Console.WriteLine("Strawberry Icecream Added");
        }

        public void AddTopping()
        {
            Console.WriteLine("This is not topping");
        }
    }
}
