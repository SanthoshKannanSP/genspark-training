using System.Runtime.InteropServices;

public class Program4
{
    private String _username = "Admin";
    private String _password = "pass";
    private int _numOfTries = 3;
    public void Run()
    {
        int attempted;
        for (attempted = 0; attempted < _numOfTries; attempted++)
        {
            if (ValidateCredentials())
                break;
        }
        if (attempted >= _numOfTries)
            Console.Write("Invalid attempts for 3 times. Exiting....");
        else
            Console.WriteLine("Successfully logged in");
    }

    private bool ValidateCredentials()
    {
        Console.Write("Enter username: ");
        String enteredUsername = Console.ReadLine().Trim();
        Console.Write("Enter password: ");
        String enteredPassword = Console.ReadLine().Trim();

        if (enteredUsername != _username || enteredPassword != _password)
        {
            Console.WriteLine("Invalid username or password!\n");
            return false;
        }

        return true;
    }
}