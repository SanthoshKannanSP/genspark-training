using assignment_1.Factory;
using assignment_1.Singleton;

Console.WriteLine("Message System");
string userChoice = string.Empty;
do
{
    Console.WriteLine("\n1. Send Email");
    Console.WriteLine("2. Send SMS");
    Console.WriteLine("3. Exit");
    userChoice = Console.ReadLine().Trim();
    ExecuteUserChoice(userChoice);
} while (userChoice != "3");

LogFileWriter.GetInstance().Dispose();

void ExecuteUserChoice(string userChoice)
{
    switch (userChoice)
    {
        case "1" or "2":
            MessageFactory messageFactory = Initialize(userChoice);
            messageFactory.Send();
            break;
        case "3":
            break;
        default:
            Console.WriteLine("Enter a valid choice!");
            break;
    }
}

static MessageFactory Initialize(string factoryChoice)
{
    if (factoryChoice == "1")
        return new EmailSender();
    return new SmsSender();
}