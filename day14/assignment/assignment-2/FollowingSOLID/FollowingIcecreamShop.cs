using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using assignment_2.FollowingSOLID.Models;

namespace assignment_2.FollowingSOLID
{
    internal class FollowingIcecreamShop
    {
        private readonly IBillingSystem _billingSystem;
        private readonly Kitchen _kitchen;
        private readonly Storage _storage;
        private readonly Waiter _waiter;

        public FollowingIcecreamShop(IBillingSystem billingSystem)
        {
            _billingSystem = billingSystem;
            _kitchen = new Kitchen();
            _waiter = new Waiter();
            _storage = new Storage();
        }

        public void ServiceCustomer()
        {
            Order customerOrder = _waiter.TakeOrder();
            var ingredients = _storage.GetIngredients(customerOrder);
            _kitchen.MakeOrder(ingredients);
            _billingSystem.createBill(ingredients);
        }

        
    }
}
