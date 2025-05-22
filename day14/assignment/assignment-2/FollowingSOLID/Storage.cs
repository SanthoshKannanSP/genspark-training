using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using assignment_2.FollowingSOLID.Models.Flavors;
using assignment_2.FollowingSOLID.Models.Interfaces;
using assignment_2.FollowingSOLID.Models.Toppings;

namespace assignment_2.FollowingSOLID
{
    internal class Storage
    {
        public List<Ingredients> GetIngredients(Order customerOrder)
        {
            List<Ingredients> ingredients = new List<Ingredients>();
            var icecream = GetIcecream(customerOrder.flavor);
            ingredients.Add(icecream);
            var toppings = GetTopping(customerOrder.topping);
            ingredients.Add(toppings);
            return ingredients;
        }

        private Icecream GetIcecream(Flavors flavor)
        {
            Icecream icecream = null;
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

        private Topping GetTopping(Toppings toppings)
        {
            Topping topping = null;
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
    }
}
