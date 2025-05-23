using assignment_2;
using assignment_2.Models;

Console.WriteLine("Select User: ");
Console.WriteLine("1. Admin");
Console.WriteLine("2. User");
Console.WriteLine("3. Guest");
Console.Write("Enter choice: ");

int userChoice;
while (!int.TryParse(Console.ReadLine(), out userChoice))
    Console.Write("Enter a valid choice: ");

Console.Write("Enter username: ");
string username = Console.ReadLine().Trim();

User user = new User();
user.Username = username;
user.Role = (UserRole)userChoice;

var fileReader = new FileReader(user);
fileReader.ReadData();
