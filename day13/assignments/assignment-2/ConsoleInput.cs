using System.Diagnostics.CodeAnalysis;
using assignment_2.Models;

namespace assignment_2
{
    public static class ConsoleInput
    {
        public static int? GetIntFromUser(string entity, bool nullable = false)
        {
            int intVal;
            Console.Write($"Enter {entity}: ");
            string userInput = Console.ReadLine().Trim();
            while (!int.TryParse(userInput, out intVal))
            {
                if (nullable && string.IsNullOrEmpty(userInput))
                    return null;
                Console.Write($"Enter a valid number!\nEnter {entity}: ");
                userInput = Console.ReadLine().Trim();

            }

            return intVal;
        }
        public static string? GetStringFromUser(string entity, bool nullable = false)
        {
            var userInput = "";
            Console.Write($"Enter {entity}: ");
            userInput = Console.ReadLine().Trim();
            while (string.IsNullOrEmpty(userInput))
            {
                if (nullable && string.IsNullOrEmpty(userInput))
                    return null;
                Console.Write($"{entity} cannot be empty.\nEnter a {entity}:");
                userInput = Console.ReadLine().Trim();
            }
            return userInput;
        }

        public static Range<int>? GetIntRangeFromUser(string entity)
        {
            int? minVal, maxVal;
            minVal = GetIntFromUser($"minimum {entity}", true);
            maxVal = GetIntFromUser($"maximum {entity}", true);
            while (maxVal <= minVal)
            {
                Console.WriteLine($"Maximum {entity} can't be lesser than minimum {entity}");
                maxVal = GetIntFromUser($"maximum {entity}", true);
            }

            if (minVal == null && maxVal == null)
                return null;
            var range = new Range<int>();
            range.MinVal = minVal ?? 0;
            range.MaxVal = maxVal ?? int.MaxValue;
            return range;
        }
    }
}
