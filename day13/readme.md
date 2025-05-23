# Day 13 - May 21th, 2025
## Session Overview
- Null Coalescing Operator
- Repository Pattern
- Abstract class
- User-defined Exception

## Null Coalescing Operator
[Reference](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/null-coalescing-operator)
- Used to provide a default value when dealing potentially null expressions
```
var result = value ?? defaultValue;
```
- If `value` is not `null`, then it is assigned to `result`. If the value is `null`, then the `defaultValue` is assigned to `result`
- The `defaultValue` expression is only evaluated if the `value` expression is evaluated to `null`
- **Null Coalescing Assignment** - Assignment operator that only assigns the right hand operand if the left hand operand is `null`
```
name ??= defaultValue;
```
- If the `name` is `null`, then `defaultValue` is assigned to `name`. Else the assignment is ignored and `name` retains it's value

## Repository Pattern
[Reference](https://learn.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application)
- Provides an abstract of the data access layer
- Acts as an intermediary between data access layer and business logic layer, decoupling the business logic from the specifics of how data is stored and retrieved
- A repository interface with methods for standard CRUD is created. This acts as the contract for data operations
- For each entity, a repository class is created that implements the repository interface and implements the CRUD operations
- The repository class will contain the actual code to work with the data store (database, files, etc.) and return the required collection of entities
- The business logic layer will work with the repository interface and not directly with the concrete repository class of the entity (dependency inversion)
- This allows the underlying data access stratergy to change without affecting the business logic
```
// Entity
public class Employee
{
    public int Id { get; set; }
    public int Age { get; set; }
    public string Name { get; set; }
    public double Salary { get; set; }
}

// Repository Interface
public interface IRepository
{
    Employee Add(Employee employee);
    Employee Update(Employee employee);
    Employee Delete(int id);
    Employee GetById(int id);
    List<Employee> GetAll();
}

// Concrete Repository
public class EmployeeRepository : IRepository
{
    private readonly AppDbContext _context;
    public EmployeeRepository(AppDbContext context)
    {
        _context = context;
    }
    // Implementing the CRUD operations as specified by the IRepository
}

// Used in business logic
public class EmployeeService
{
    IRepository _employeeRepository;
    public EmployeeService(IRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }
    public List<Employee> GetAllEmployees()
    {
        try
        {
            var employees = _employeeRepository.GetAll());
            return employees;
        }
        catch (Exception e)
        {
            return null;
        }
    }
}
```
- The repository interface could be made generic so that the code can be used by multiple entities
```
// Repository Interface
public interface IRepository<K, T> where T : class
{
    T Add(T item);
    T Update(T item);
    T Delete(K id);
    T GetById(K id);
    List<T> GetAll();
}

// Employee Repository
public class EmployeeRepository : IRepository<int, Employee>
{
    // Implementations
}

// Product Repository
public class ProductRepository : IRepository<int, Product>
{
    // Implementations
}
```
- Instead of the concrete entity repositries directly implementing the repository interface, an abstract repository could be used as an intermediatry to encapsulate generic implementation of CRUD operations, allowing entity-specific repositories to inherit and extend only when necessary.
```
public abstract class AbstractRepository<K,T> : IRepository<K,T> where T : class
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public T GetById(K id) => _dbSet.Find(id);
    // Implementing other CRUD operations
}
```
## Abstract Class
[Reference](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/abstract-and-sealed-classes-and-class-members)
- Create a partial class and class members that must be implemented in a derived class
```
public abstract class A
{
    // Class members
}
```
- An abstract class cannot be instantiated
- Used to provide a common definition that multiple derived classes can share
- Contains abstract methods that have no implementation (abstract methods can only be used in abstract classes)
```
public abstract class A
{
    public abstract void DoWork(int i);
}
```
- Any derived classes from an abstract class should implement all abstract methods or be an abstract class itself

## User-defined Exception
[Reference](https://learn.microsoft.com/en-us/dotnet/standard/exceptions/how-to-create-user-defined-exceptions)
- User-defined exceptions can be created by inheriting the `Exception` class
- It is best practice to end the class name with "Exception" and implement the three common constructors
```
public class EmployeeNotFoundException : Exception
{
    public EmployeeNotFoundException()
    {
    }
    
    public EmployeeNotFoundException(string message)
        : base(message)
    {
    }

    public EmployeeNotFoundException(string message, Exception inner)
        : base(message, inner)
    {
    }
    
}
```