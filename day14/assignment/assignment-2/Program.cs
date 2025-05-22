using assignment_2.ViolatingSOLID;
using assignment_2.FollowingSOLID;

ViolatingIcecreamShop violatingIceCreamShop = new ViolatingIcecreamShop();
//violatingIceCreamShop.ServiceCustomer();

var physicalBillingSystem = new PhysicalBillingSystem();
var emailBillingSystem = new EmailBillingSystem();
FollowingIcecreamShop followingIcecreamShop = new FollowingIcecreamShop(physicalBillingSystem);
followingIcecreamShop.ServiceCustomer();