string userChoice = "";
EmployeeDirectory employeeDirectory = new EmployeeDirectory();
EmployeePromotion employeePromotion = new EmployeePromotion();
do
{
    Console.WriteLine();
    Console.WriteLine("Employee Console: ");
    Console.WriteLine("1. Add new employee");
    Console.WriteLine("2. View all employees");
    Console.WriteLine("3. Modify Promotion List");
    Console.WriteLine("4. Find employee position in promotion list");
    Console.WriteLine("5. Free Excess Memory in promotion list");
    Console.WriteLine("6. Promote Employees");
    Console.WriteLine("7. Get Employee by Id");
    Console.WriteLine("8. Sort Employees by Salary");
    Console.WriteLine("9. Find employees by Name");
    Console.WriteLine("10. Find Elder employees");
    Console.WriteLine("11. Modify Employee");
    Console.WriteLine("12. Delete Employee by Id");
    userChoice = Console.ReadLine().Trim();
    ExecuteUserChoice(userChoice);
} while (userChoice.Any());

void ExecuteUserChoice(string userChoice)
{
    switch (userChoice)
    {
        case "1":
            AddEmployee();
            break;
        case "2":
            DisplayAllEmployees();
            break;
        case "3":
            ModifyEmployeePromotion();
            break;
        case "4":
            FindEmployeePromotionPosition();
            break;
        case "5":
            FreeExcessMemoryOfPromotionList();
            break;
        case "6":
            PromoteEmployees();
            break;
        case "7":
            GetEmployeeById();
            break;
        case "8":
            SortEmployeeBySalary();
            break;
        case "9":
            FindEmployeesByName();
            break;
        case "10":
            EmployeesElderThan();
            break;
        case "11":
            ModifyEmployeeDetail();
            break;
        case "12":
            DeleteEmployee();
            break;
        case "":
            Console.WriteLine("Exiting.....");
            break;
        default:
            Console.WriteLine("Enter a valid choice");
            break;
    }
}

void DeleteEmployee()
{
    Console.WriteLine("Enter Employee Id");
    int employeeId;
    while (!int.TryParse(Console.ReadLine(), out employeeId))
        Console.WriteLine("Enter a valid number!");
    var employee = employeeDirectory[employeeId];
    if (employee == null)
        Console.WriteLine($"No employees with Id {employeeId} found!");
    else
    {
        employeeDirectory.Delete(employeeId);
        Console.WriteLine($"Employee with Id {employeeId} successfully deleted");
    }
}

void ModifyEmployeeDetail()
{
    Console.WriteLine("Enter Employee Id");
    int employeeId;
    while (!int.TryParse(Console.ReadLine(), out employeeId))
        Console.WriteLine("Enter a valid number!");
    var employee = employeeDirectory[employeeId];
    if (employee == null)
        Console.WriteLine($"No employees with Id {employeeId} found!");
    else
    {
        Console.WriteLine("Please enter the employee name");
        var name = Console.ReadLine();
        Console.WriteLine("Please enter the employee age");
        var age = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Please enter the employee salary");
        var salary = Convert.ToDouble(Console.ReadLine());
        employee.Name = name;
        employee.Age = age;
        employee.Salary = salary;
        employeeDirectory[employeeId] = employee;
    }
}

void EmployeesElderThan()
{
    Console.WriteLine("Enter Employee Id:");
    int employeeId;
    while (!int.TryParse(Console.ReadLine(), out employeeId))
        Console.WriteLine("Enter a valid number!");
    var employee = employeeDirectory[employeeId];
    if (employee == null)
        Console.WriteLine($"No employees with Id {employeeId} found!");
    else
    {
        var elderEmployees = employeeDirectory.GetElderEmployees(employee);
        if (elderEmployees.Any())
        {
            foreach (var elderEmployee in elderEmployees)
                Console.WriteLine(elderEmployee.ToString() + "\n");
        }
        else
        {
            Console.WriteLine("No elder employee found!");
        }
    }
}

void FindEmployeesByName()
{
    Console.WriteLine("Enter employee name");
    var employeeName = Console.ReadLine().Trim();
    var employeeList = employeeDirectory[employeeName];
    if (employeeList == null)
        Console.WriteLine($"No employees with name {employeeName} found!");
    else
        foreach( var employee in employeeList )
            Console.WriteLine(employee.ToString()+"\n");
}

void SortEmployeeBySalary()
{
    if (employeeDirectory.Count == 0)
    {
        Console.WriteLine("No employees to sort");
        return;
    }
    Console.WriteLine("Employees sorted by salary");
    employeeDirectory.SortEmployees();
}

void GetEmployeeById()
{
    Console.WriteLine("Enter Employee Id:");
    int employeeId;
    while (!int.TryParse(Console.ReadLine(), out employeeId))
        Console.WriteLine("Enter a valid number");

    var employee = employeeDirectory[employeeId];
    if (employee == null)
        Console.WriteLine($"Employee with Id {employeeId} doesn't exist");
    else
        Console.WriteLine(employee.ToString());

}

void PromoteEmployees()
{
    Console.WriteLine("Promoted employee list: ");
    employeePromotion.PromoteEmployees();
}

void FreeExcessMemoryOfPromotionList()
{
    var currentMemorySize = employeePromotion.GetCurrentMemorySize();
    Console.WriteLine($"The current size of the collection is {currentMemorySize}");
    employeePromotion.FreeExcessMemory();
    currentMemorySize = employeePromotion.GetCurrentMemorySize();
    Console.WriteLine($"The size after removing the extra space is {currentMemorySize}");
}

void FindEmployeePromotionPosition()
{
    Console.WriteLine("Please enter the name of the employee to check promotion position");
    var employeeName = Console.ReadLine().Trim();
    employeePromotion.FindPromotionPosition(employeeName);
}

void ModifyEmployeePromotion()
{
    employeePromotion.ClearPromotionList();
    employeePromotion.CreatePromotionList(employeeDirectory);
}

void AddEmployee()
{
    Employee newEmployee = new Employee();
    newEmployee.TakeEmployeeDetailsFromUser();
    employeeDirectory.AddEmployee(newEmployee);
}

void DisplayAllEmployees()
{
    if (employeeDirectory.Count == 0)
    {
        Console.WriteLine("No employees added");
        return;
    }
    Console.WriteLine("All Employees: ");
    employeeDirectory.DisplayEmployees();
}