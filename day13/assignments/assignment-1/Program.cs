using assignment_1.Models;
using assignment_1.Repositories;
using assignment_1.Services;

EmployeeRepository employeeRepository = new EmployeeRepository();
EmployeeService employeeService = new EmployeeService(employeeRepository);
string userChoice = "";

do
{
    Console.WriteLine("Employee Portal (leave blank to exit)");
    Console.WriteLine("1. View All Employees");
    Console.WriteLine("2. Add a new employee");
    Console.WriteLine("3. Employee Search");
    userChoice = Console.ReadLine().Trim();
    ExecuteUserChoice(userChoice);
} while (userChoice.Any());

void ExecuteUserChoice(string? userChoice)
{
    
    switch (userChoice)
    {
        case "1":
            var employees = employeeService.GetAllEmployees();
            if (employees == null)
            {
                Console.WriteLine("No Employees found!");
                break;
            }
            foreach (var employee in employees)
                Console.WriteLine(employee);
            break;
        case "2":
            Employee newEmployee = new Employee();
            newEmployee.TakeEmployeeDetailsFromUser();
            employeeService.AddEmployee(newEmployee);
            break;
        case "3":
            SearchModel searchModel = new SearchModel();
            searchModel.GetSearchParamsFromUser();
            var searchedEmployees = employeeService.SearchEmployee(searchModel);
            if (searchedEmployees == null)
            {
                Console.WriteLine("No employee matched with the search criteria");
                break;
            }
            foreach (var employee in searchedEmployees)
            {
                Console.WriteLine(employee);
            }
            break;
        case "":
            Console.WriteLine("Exiting....");
            break;
        default:
            Console.WriteLine("Enter a valid choice!");
            break;
    }
}