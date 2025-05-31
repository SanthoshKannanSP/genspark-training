# Day 14 - May 22nd, 2025
## Session Overview
- Delegates
- Anonymous Methods
- Lambda expressions
- Predicate expressions
- Extension Methods
- SOLID principles

## Delegates
[Reference](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/delegates/)
- Special type that represents references to methods with a particular signature (parameter list and return type)
- A delegate instance could be assigned methods with the same signature as the delegate
- All the methods that are assigned to a delegate instance can be called by calling the delegate instance
```cs
public delegate void PerformCalculation(int num1, int num2);

public void Add(int num1, int num2)
{
    var result = num1 + num2;
    Console.WriteLine($"The sum of two numbers is {result}");
}

public void Product(int num1, int num2)
{
    var result = num1 * num2;
    Console.WriteLine($"The product of two numbers is {result}");
}

PerformCalculation del;
del += Add;
del += Product;

del(5,19);
```

## Anonymous methods
[Reference](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/delegates/delegates-with-named-vs-anonymous-methods)
- Methods with no name
- Used to directly assign a code block to a delegate inside of creating a named function and then assigning it
- The `delegate` operator is used to create an anonymous function that can be converted to a delegate type
```cs
del += delegate (int n1, int n2)
{
    Console.WriteLine($"The difference of two numbers is {n2 - n1}");
};
```

## Lambda Expressions
[Reference](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/lambda-expressions#expression-lambdas)
- Lambda expressions provide a more concise way to create an anonymous function
- **Lambda declaration operator:** `=>`
- A lambda expression can be created by specifying the input parameters (if any) on the left side and an expression or statement block on the right side of the `=>` operator
```cs
// Expression lambdas
del += (int n1, int n2) => Console.WriteLine($"The difference of two numbers is {n2 - n1}");

// Statement lambdas
del += (int n1, int n2) => {
    var result = n2 - n1;
    Console.WriteLine($"The difference of two numbers is {result}");
};
```
- Lambda expression with zero input parameters can be specified by empty parentheses
- For lambda expressions with a single parameter, the parentheses are optional
- Lambda expression can be used in methods that requires instances of delegate type
```cs
int[] numbers = { 2, 3, 4, 5 };
var squaredNumbers = numbers.Select(x => x * x);
```

## Predicate Expressions
[Reference](https://learn.microsoft.com/en-us/dotnet/api/system.predicate-1?view=net-9.0)
- Delegate that represents a method that determines whether the specified object meets a criteria
```cs
public void FindEmployee()
{
    int empId = 102;
    Predicate<Employee> predicate = e => e.Id == empId;
    Employee? emp = employees.Find(predicate);
    Console.WriteLine(emp.ToString() ?? "No such employee");
}
```
- Predicates are typically represented by lambda expression
```cs
Employee? emp = employees.Find(e => e.Id == empId);
```

## Extension Members
[Reference](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/extension-methods)
- Add methods to existing types without creating a new derived type, recompiling or modifying the original type
- Extension members should be defined inside a non-nested, non-generic static class
- Declaring extension members using `this` modifier to the first parameter
```cs
public static class ExtensionFunctions
{
    public static bool NameValidationCheck(this string name)
    {
        if (name.All(char.IsLetter) && name.Substring(0,1).All(char.IsUpper))
            return true;
        return false;
    }
}
```
- Beginning with C#14, extension members can be declared using *extension blocks*
```cs
public static class ExtensionFunctions
{
    extension(string name)
    {
        public bool NameValidationCheck()
        {
            if (name.All(char.IsLetter) && name.Substring(0,1).All(char.IsUpper))
                return true;
            return false;
        }
    }
}
```
- Extension members can be called like accessing instance members
```cs
foreach (Employee employee in employees)
{
    if (employee.Name.NameValidationCheck())
        Console.WriteLine(employee.ToString());
    else
        Console.WriteLine($"Employee name {employee.Name} is not valid");
}
```
- An extension member cannot override behavior defined in a class. An extension member with the same name and signature as a class member are not called. This is because extension members have lower priority than class members at compile time. When the compiler encounters a member invocation, it first looks for a match in the type. If no match is found, the compiler only then searches for any extension members for that type. The compiler binds the first matching member it finds

## SOLID principles
[Reference](https://www.youtube.com/watch?v=rtmFCcjEgEw)
- Make code more maintainable
- Make it easier to extend the system with new functionality without breaking existing ones
- Make code easier to read and understand, thus spending less time figuring out what it does and more time on actually developing the solution

### Single Responsibility Principle
- A class should have only one responsibility - only one reason to change
- **Reason:** If a class has multiple responsibilities, it increases the possibility of bugs as changes in one of its responsibilities could affect the other without knowing. Breaking classes down to smaller and more focused units makes code easier to understand, maintain and test
- *Violating Example:* A `UserService` that takes a `Request` with new user data, validates the data, create the new `User` object, saves it, and returns the newly created `User` object as `Response`.
- *Following Example:* It is better to split the `UserService` into a `UserValidation` class that validates data to create `User` object and a `UserRepository` to create and save the `User` object. The `UserService` can use these new classes to respond to the request.

### Open/Closed Principle
- A class should be open for extension but closed for modification - extend functionality by adding new code instead of changing existing code
- **Reason:** If we have to modify the existing code to add new functionalities, it is possible that the changes breaks the existing code and the other classes dependent on it
- *Violating Example:* A `Payment` method that takes a string representing payment type (Cash on Delivery and Credit Card) and the amount. If...Else statement are used to conditionally execute the `PayWithCash` method or `PayWithCreditCard` method based on the payment type. To add another payment option (Gpay), we have to *modify* the `Payment` method If...Else statement to add the `PayWithGpay` method execution if the payment type is Gpay.
- *Following Example:* A `PaymentFactory` is used that will create the necessary payment class based on the payment type. The `Payment` method can use the payment class returned by the `PaymentFactory` irrespective of what the payment type is. Hence even if a new payment type is added in the future, the `Payment` method will not change in order to accomodate the new functionality.

### Liskov Substitution Principle
- Any derived class should be able to substitute its parent class without the consumer knowing and without affecting the correctness of the program
- **Reason:** To enforce consistency and reliability when using polymorphism
- *Violating Example:* A `BankAccount` should have the methods to `CheckBalance` and `Withdraw` amount. Both `SavingsAccount` and `FixedDepositAccount` are childrens of `BankAccount`. Although both class can implement the `CheckBalance` method, the `FixedDepositAccount` cannot implement the `Withdraw` method. Hence, in a method that requires a `BankAccount` object, if we substitute `FixedDepositAccount` object, calling the `Withdraw` function breaks the code
- *Following Example:* A `BankAccount` should have the method to `CheckBalance` amount. A `WithdrawableAccount` is a child of `BankAccount` and should have the method to `Withdraw` ammount. `FixedDepositAccount` is a child of `BankAccount` and `SavingsAccount` is a child of `WithdrawableAccount`. Now, if we substitute either of the classes' object to a method that requires `BankAccount` object, nothing will break as both objects have the `CheckBalance` method.

### Interface Segregation Principle
- No client should be forced to depend on methods it does not use
- **Reason:** If classes are forced to implement methods that they don't need, it leads to bloated classes that have several useless methods. Further, it can cause runtime errors like `NotImplementedError`.
- *Violating Example:* An `Ingredient` interface has ingredient `Name`, `AddIcecream` method and `AddToppings` method. The `ChocolateIcecream` implements the `Ingredient` interface, but doesn't need the `AddToppings` method and hence leaves it unimplemented. The `OreosToppings` implements the `Ingredient` interface, but doesn't need the `AddIcecream` method and hence leaves it unimplemented.
- *Following Example:* An `Ingredient` interface has ingredient `Name`. The `Icecream` interface implements the `Ingredient` interface and has `AddIcecream` method. Similarly, the `Toppings` interface implements the `Ingredient` interface and has `AddToppings` method. The `ChocolateIcecream` implements `Icecream` interface and `OreosToppings` implements `Toppings` interface. No unnecessary method has been implemented.

### Dependency Inversion Principle
- High-level modules should not depend on low-level modules. Both should depend on abstractions
- **Reason:** The low-level module implementation can be changed without altering the high-level code (loose coupling)
- *Violating Example:* Consider a `NotificationService` that sends any event notification to the customer. The `NotificationService` has an instance of `EmailSender` and uses it to send the notification. The `NotificationService` and `EmailSender` are tightly coupled. If we want to change the notification to be a SMS using `SmsSender` instead of email, all the code in the `NotificationService` has to be changed.
- *Following Example:* The `NotificationService` uses the abstraction of `NotificationSender` interface in it's code. The `EmailSender` and `SmsSender` both implement the `NotificationSender` interface. Now `EmailSender` and `SmsSender` can be swapped out as needed without affecting the existing code in `NotificationService`