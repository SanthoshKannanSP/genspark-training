using assignment_1.Misc;
using assignment_1.Models;

MyDelegate del =  Add;
del += Product;
GenericDelegate<int> genericDelegate = Add;

// Anonymous method
del += delegate (int n1, int n2)
{
    Console.WriteLine($"The difference of two numbers is {n2 - n1}");
};

// Lambda expression
del += (int n1, int n2) => Console.WriteLine($"The two numbers are {n1} and {n2}");

del(5, 10);

List<Employee> employees = new();
Employee employee1 = new Employee() { Id = 101, Name="Ram", Age=24 };
Employee employee2 = new Employee() { Id = 102, Name = "1arun", Age = 22 };
Employee employee3 = new Employee() { Id = 102, Name = "varun", Age = 22 };
employees.Add(employee1);
employees.Add(employee2);
employees.Add(employee3);

FindEmployee();
SortEmployee();

// Extension functions
Console.WriteLine("\nValidation Checks");
foreach (Employee employee in employees)
{
    if (employee.Name.NameValidationCheck())
        Console.WriteLine(employee.ToString());
    else
        Console.WriteLine($"Employee name {employee.Name} is not valid");
}

void FindEmployee()
{
    int empId = 102;
    Predicate<Employee> predicate = e => e.Id == empId;
    Employee? emp = employees.Find(predicate);
    Console.WriteLine(emp.ToString() ?? "No such employee");
}
void SortEmployee()
{
    var sortedEmployees = employees.OrderBy(e => e.Name);
    foreach (var emp in sortedEmployees)
    {
        Console.WriteLine(emp.ToString());
    }
}

void Add(int num1, int num2)
{
    var result = num1 + num2;
    Console.WriteLine($"The sum of two numbers is {result}");
}

void Product(int num1, int num2)
{
    var result = num1 * num2;
    Console.WriteLine($"The product of two numbers is {result}");
}

public delegate void MyDelegate(int num1, int num2);
public delegate void GenericDelegate<T>(T num1, T num2);
