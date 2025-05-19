
public class Program12
{
    public void Run()
    {
        Console.WriteLine("Please enter a message (only lowercase letters, no symbols or digits)");
        var userMessage = Console.ReadLine().Trim();

        while(!userMessage.All(ch => ch >= 'a' && ch <= 'z'))
            Console.WriteLine("Enter a valid message");

        var encryptedMessage = Encrypt(userMessage);
        var decryptedMessage = Decrypt(encryptedMessage);

        Console.WriteLine($"Encrypted Message: {encryptedMessage}");
        Console.WriteLine($"Decrypted Message: {decryptedMessage}");
    }

    private string Decrypt(string userMessage)
    {
        var decryptedMessage = "";
        foreach(var ch in userMessage)
        {
            decryptedMessage += (char)((ch-'a'-3+26)%26 + 'a');
        }
        return decryptedMessage;
    }

    private string Encrypt(string userMessage)
    {
        var encryptedMessage = "";
        foreach(var ch in userMessage)
        {
            encryptedMessage += (char)((ch-'a'+3)%26 + 'a');
        }
        return encryptedMessage;
    }
}