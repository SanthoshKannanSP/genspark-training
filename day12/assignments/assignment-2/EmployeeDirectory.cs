class EmployeeDirectory
{
    private Dictionary<int, Employee> employees = new Dictionary<int, Employee>();

    public void AddEmployee(Employee employee)
    {
        if (employees.ContainsKey(employee.Id))
        {
            Console.WriteLine($"Employee with ID {employee.Id} already exists");
            return;
        }

        employees.Add(employee.Id, employee);
        Console.WriteLine("Employee added successfully!");
    }

    public void DisplayEmployees()
    {
        foreach (var employee in employees.Values)
            Console.WriteLine(employee.ToString()+"\n");
    }

    public bool HasEmployee(string name)
    {
        return employees.Values.Any(employee => employee.Name == name);
    }

    public void SortEmployees()
    {
        var employeeList = employees.Values.ToList();
        employeeList.Sort();
        foreach(var employee in employeeList)
            Console.WriteLine(employee.ToString()+"\n");
    }

    public List<Employee> GetElderEmployees(Employee employee)
    {
        return employees.Values.Where(e => e.Age > employee.Age).ToList();
    }

    public void Delete(int employeeId)
    {
        employees.Remove(employeeId);
    }

    public Employee this[int i]
    {
        get
        {
            if (employees.ContainsKey(i))
                return employees[i];
            return null;
        }

        set
        {
            employees[i] = value;
        }
    }

    public List<Employee> this[string name]
    {
        get
        {
            var employeelist = employees.Values.Where(employee => employee.Name == name);
            if (employeelist.Any())
                return employeelist.ToList();
            return null;
        }
    }

    public int Count { get { return employees.Count; } }
}