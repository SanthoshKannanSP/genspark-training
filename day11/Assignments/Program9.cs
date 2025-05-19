public class Program9
{
    string _secretWord = "GAME";
    public void GameStart()
    {
        Console.WriteLine("A secret 4-letter word has been choosen. Let's see if you can guess it!");
    }

    public void GameLoop(int guessesMade, out bool hasGameEnded)
    {
        Console.WriteLine($"Guesses made: {guessesMade}");

        var userGuess = "";
        do
        {
            Console.Write("Please enter a 4-letter word: ");
            userGuess = Console.ReadLine().Trim().ToUpper();
        } while (userGuess.Length != 4);

        var correctLetters = ValidateGuess(userGuess);
        if (correctLetters == 4)
            hasGameEnded = true;
        else
            hasGameEnded = false;
    }

    public void GameEnd(int guessesMade)
    {
        Console.WriteLine($"Congratulation! You have found the secret word {_secretWord} in {guessesMade} attempts!");
    }

    public int ValidateGuess(string userGuess)
    {
        int correctLetters = 0;
        for(int i=0;i<4;i++)
        {
            if (userGuess[i] == _secretWord[i])
            {
                correctLetters += 1;
                Console.Write(userGuess[i]);
            }
            else
                Console.Write('*');
        }
        Console.WriteLine();
        return correctLetters;
    }

    public void Run()
    {
        int guessesMade = 0;
        GameStart();
        bool hasGameEnded = false;
        do
        {
            GameLoop(guessesMade++, out hasGameEnded);
        } while (!hasGameEnded);
        GameEnd(guessesMade);
    }
}