# Day 15 - May 23rd, 2025
## Session Overview
- Generation levels
- Design Patterns
- Singleton Pattern
- Factory Pattern
- Abstract Factory Pattern
- Adapter Pattern
- Flyweight Pattern

## Generation Levels
[Reference](https://learn.microsoft.com/en-us/dotnet/standard/garbage-collection/fundamentals#generations)
- The garabage collection algorithm divides the heap memory into 3 generation levels - *Generation 0*, *Generation 1* and *Generation 2*
- The 3 Generation levels are based on the consideration that
    - It is faster to compact the memory for a portion of the heap than the entire heap
    - Newer objects have shorter lifetime and older objects have longer lifetime
    - Newer objects tend to be related to each other and accessed by the application around the same time
- To optimize the performance of garbage collector, the heap is divided into 3 Generation levels to manage long-lived and short-lived objects separately
- **Generation 0:** Contains the youngest and short-lived objects (*EG: Temporary variable*). Garbage collection occurs frequently in this generation. If a new object is created when the *Generation 0* is full, the garbage collector performs collection to free up address space for the object. The garbage collector starts by examining the *Generation 0* rather than all objects in heap as collection of *Generation 0* often reclaims enough memory to continue creating new object.
- **Generation 1:** Contains objects promoted from *Generation 0* and acts as a buffer between short-lived and long-lived objects. Objects that survive the collection of *Generation 0* are memory compacted and promoted to *Generation 1*. If a new object is created and the collection of *Generation 0* doesn't reclaim enough memory, then the garbage collector will perform collection of *Generation 1*.
- **Generation 2:** Contains objects promoted from *Generation 1*. This generation contains long-lived objects (*EG: Server application that contains static data that lives for the duration of the process*). Objects that survice the collection of *Generation 1* are memory compacted and promoted to *Generation 2*. Objects in *Generation 2* that survive a collection remain in *Generation 2* until there are no more active references to that object in the program.

## Design Patterns
[Reference](https://refactoring.guru/design-patterns/what-is-pattern)
- Typical solutions to commonly occuring problems in software design
- First influential book on design patterns - [Design Patterns: Elements of Reusable Object-Oriented Software](https://www.goodreads.com/book/show/85009.Design_Patterns)
- Can be classified into three types based on intent - *Creational Pattern*, *Structural Pattern* and *Behavioral Pattern*

## Singleton Pattern
[Reference](https://refactoring.guru/design-patterns/singleton)
- Created a class that has only one instance while providing a global access point to this instance
- **Problem Solved:** Controlling access to a shared resource which needs to have a global access point to it. Minimizes resource usage by reusing the same instance instead of creating new ones.
- **Implementation:**
    - Making the constructor private to prevent other objects from using the `new` operator to create a new instance of the class
    - Create a static field for storing the Singleton instance
    - Create a static method that acts as an access point to get the Singleton instance
    - Implement "Lazy initialization" inside the static method - create a new object by calling the private constructor if it is the first call and return that instance for all subsequent calls.
    - Replace all calls of Singleton constructor method with calls to its static creation method
- **Example:**
```cs
public class LogFileWriter
{
    // Static field to store the Singleton object
    private static LogFileWriter _logFileWriter;

    private StreamWriter _writer;

    // Private constructor to prevent other classes from creating a new object
    private LogFileWriter() 
    {
        _writer = new StreamWriter("log.txt", true);
    }

    // Static creation method that acts as a global access point to the Singleton instance
    public static LogFileWriter GetInstance()
    {
        if (_logFileWriter == null)
            _logFileWriter = new LogFileWriter();
        return _logFileWriter;
    }

    // Methods that execute some business logic
}

public class Program
{
    // Getting the Singleton instance through the static method
    var _writer = LogFileWriter.GetInstance();

    // Business logic
}
```
- **Disadvantages:** Violates Single Responsibility Principle, requires special treatment in multithreaded environment so that multiple threads won't create a Singleton object several times and difficult to unit test client code of the Singleton

## Factory Pattern
- Provides an interface for creating objects, but lets subclasses or methods to decide which class to instantiate
- **Problem Solved:** Create different types of objects based on input or context, without exposing the creation logic. Decouples object creation from usage, and makes it easier to add new types.
- **Implemention:**
    - Make all products returned by the Factory to implement the same interface
    - Create a Creator class with an abstract factory method. The return type should match the common product interface
    - Create a set of Creator subclasses for each type of product returned by the factory. Override the abstract factory method in the subclasses and implement the necessary creation code
- **EG:**
```cs
// Creater class
public abstract class MessageFactory
{
    // Factory method
    public abstract IMessage CreateMessage();

    public void Send()
    {
        IMessage message = CreateMessage();
        message.SendMessage();
    }
}

// Subclasses of Creator class
public class EmailSender : MessageFactory
{
    public override IMessage CreateMessage()
    {
        var emailMessage = new EmailMessage();
        Console.Write("Enter To Mail Id: ");
        emailMessage.Receiver = Console.ReadLine();
        Console.WriteLine("Enter Message: ");
        emailMessage.Content = Console.ReadLine();
        return emailMessage;
    }
}

public class SmsSender : MessageFactory
{
    public override IMessage CreateMessage()
    {
        var smsMessage = new SmsMessage();
        Console.Write("Enter Phone number: ");
        smsMessage.Receiver = Console.ReadLine();
        Console.WriteLine("Enter Message: ");
        smsMessage.Content = Console.ReadLine();
        return smsMessage;
    }
}

public class Program
{
    // Creating the concrete Creator subclass based on input
    public MessageFactory Initialize(string factoryChoice)
    {
        if (factoryChoice == "1")
            return new EmailSender();
        return new SmsSender();
    }

    public void Execute(string userChoice)
    {
        MessageFactory messageFactory = Initialize(userChoice);
        messageFactory.Send();
    }
}
```
- **Disadvantages:** Code becomes more complicated due to the introduction of a lot of new subclasses to implement the pattern

## Abstract Factory Pattern
- Provides an interface for creating a family or related objects without specifying their concrete classes
- **Problem Solved:** Enables creating a family of multiple objects that are related and easily switching between the families without changing client code
- **Implementation:**
    - Declare abstract product interfaces for all product types. Make all concrete product classes to implement these matches
    - Create abstract factory interface with a set of creation methods for all abstract products
    - Implement a set of concrete factory classes, one for each product variant
    - Create factory initialization code that instantiates one of the concrete family classes
- **EG:**
```cs
public interface GUIFactory
{
    Button createButton();

    Checkbox createCheckbox();
}

class WinFactory : GUIFactory
{
    public Button createButton()
    {
        return new WinButton();
    }

    public Checkbox createCheckbox()
    {
        return new WinCheckbox();
    }
}

class MacFactory : GUIFactory
{
    public Button createButton()
    {
        return new MacButton();
    }

    public Checkbox createCheckbox()
    {
        return new MacCheckbox();
    }
}

public class Program
{
    public void ClientMethod(GUIFactory factory)
    {
        var button = factory.createButton();
        var checkbox = factory.createCheckbox();
    }

    public void Main()
    {
        if (config.OS == "Windows") then
            factory = new WinFactory()
        else if (config.OS == "Mac") then
            factory = new MacFactory()
    }
}
```
- **Disadvantage:** Code becomes more complicated due to the introduction of a lot of new interfaces and classes to implement the pattern

## Adapter Pattern
[References](https://refactoring.guru/design-patterns/adapter)
- Enable incompatible interfaces to collaborate
- **Problem Solved:** When a existing code or third-party class has an interface you can't change, but need to use it with a different incompatible interface
- **Implementation:**
    - Have a useful *service* which you cannot change the interface of
    - Have one or more *client* classes that would benefit for using the *service* class
    - Declare the *client* interface and describe how clients would communicate with the *service*
    - Declare the adapter class and implement the *client* interface
    - Add a field to the adapter class that would store the reference to the service object
    - Implement all methods of the *client* interface in the adapter class. The adapter class should delegate most of the work to the *service* object, handling only the interface or data format conversion
    - Clients should use the adapter through the *client* interface
- **EG:**
```cs
// Third party Service
public class ThirdPartyLogger {
    public void WriteLog(string level, string message) {
        Console.WriteLine($"{level.ToUpper()}: {message}");
    }
}

// Client interface
public interface IAppLogger {
    void LogInfo(string message);
    void LogError(string message);
}

// Adapter
public class LoggerAdapter : IAppLogger {
    private readonly ThirdPartyLogger _thirdPartyLogger;

    public LoggerAdapter(ThirdPartyLogger thirdPartyLogger) {
        _thirdPartyLogger = thirdPartyLogger;
    }

    public void LogInfo(string message) {
        _thirdPartyLogger.WriteLog("info", message);
    }

    public void LogError(string message) {
        _thirdPartyLogger.WriteLog("error", message);
    }
}

public class Program
{
    static void Main() {
        IAppLogger logger = new LoggerAdapter(new ThirdPartyLogger());
        logger.LogInfo("User logged in.");
        logger.LogError("Database connection failed.");
    }
}
```
- **Disadvantage:** Code becomes complicated due to the introduction of a set of new classes and interfaces. Sometimes it's simpler to change the service class to match the client

## Flyweight Pattern
[Reference](https://refactoring.guru/design-patterns/flyweight)
- Minimizes memory usage by sharing common parts of state between multiple objects
- **Problem Solved:** When a large number of similar objects with some common state has to be created, which can be very memory intensive
- **Implementation:**
    - Divide fields of a class that will become a flyweight into two parts - the *intrinsic* state (unchanging data duplicated across several objects) and the *extrinsic* state (contextual data that is unique to the object)
    - Leave the *intrinsic* fields in the class and make them immutable. They should take their initial values only inside the constructor
    - Modify all methods that use the *extrinsic* fields by introducing a new parameter and use it instead of the fields
    - Optionally, create a factory class to manage the pool of flyweights. This ensures that the flyweights containing the *intrinsic* fields aren't duplicated for each object. The clients should request the desired flyweights through the factory by passing the *intrinsic* state to the factory
    - The client must store and calculate the *extrinsic* fields to be able to call the methods of the flyweight objects
- **EG:**
```cs

public class TreeType{
    // The name, color and texture of trees are common for a type of tree
    string name;
    string color;
    string texture;
    public TreeType(string name, string color, string texture) { ... };
    // The position of the tree is extrinsic properties, unique to each tree
    public void draw(Canvas canvas, int x, int y) {};
}

// Individual Tree objects that use the shared flyweight
public class Tree{
    int x,y;
    TreeType type;
    public Tree(int x, int y, TreeType type) { ... };
    public void draw(Canvas canvas)
    {
        type.draw(canvas, x, y);
    }
}

public class Forest{
    List<Tree> trees = new();

    public void plantTree(int x, int y, string name, string color, string texture)
    {
        // Tree Factory to create/get each type of tree
        var type = TreeFactory.getTreeType(name, color, texture);
        var tree = new Tree(x, y, type);
        trees.add(tree);
    }

    public void draw(Canvas canvas)
    {
        foreach (var tree in trees)
            tree.draw(canvas)
    }
}
```
- **Disadvantages:** Could be trading RAM usage from CPU usage and code becomes complicated due to splitting a single entity into two parts