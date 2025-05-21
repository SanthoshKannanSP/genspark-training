namespace assignment_1.Models
{
    public class SearchModel
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public Range<int>? Age { get; set; }
        public Range<double>? Salary { get; set; }

        public void GetSearchParamsFromUser()
        {
            Id = GetIdFromUser();
            Name = GetNameFromUser();
            Age = GetAgeFromUser();
            Salary = GetSalaryFromUser();
        }

        private static Range<int> GetAgeFromUser()
        {
            int minAge, maxAge;
            var userInput = "";
            Range<int> age = new Range<int>();

            Console.WriteLine("Enter minimum age to Search: ");
            userInput = Console.ReadLine().Trim();
            while (!int.TryParse(userInput, out minAge))
            {
                if (String.IsNullOrEmpty(userInput))
                {
                    minAge = 0;
                    break;
                }
                
                Console.WriteLine("Enter a valid number!");
            }

            Console.WriteLine("Enter maximum age to Search: ");
            userInput = Console.ReadLine().Trim();
            while (!int.TryParse(userInput, out maxAge))
            {
                if (String.IsNullOrEmpty(userInput))
                {
                    maxAge = int.MaxValue;
                    break;
                }
                    
                Console.WriteLine("Enter a valid number!");
            }

            if (minAge == 0 && maxAge == int.MaxValue)
                return null;
            age.MinVal = minAge;
            age.MaxVal = maxAge;
            return age;
        }

        private static Range<double> GetSalaryFromUser()
        {
            double minSal, maxSal;
            var userInput = "";
            Range<double> salary = new Range<double>();

            Console.WriteLine("Enter minimum salary to Search: ");
            userInput = Console.ReadLine().Trim();
            while (!double.TryParse(userInput, out minSal))
            {
                if (String.IsNullOrEmpty(userInput))
                {
                    minSal = 0;
                    break;
                }

                Console.WriteLine("Enter a valid number!");
            }

            Console.WriteLine("Enter maximum salary to Search: ");
            userInput = Console.ReadLine().Trim();
            while (!double.TryParse(userInput, out maxSal))
            {
                if (String.IsNullOrEmpty(userInput))
                {
                    maxSal = double.MaxValue;
                    break;
                }

                Console.WriteLine("Enter a valid number!");
            }

            if (minSal == 0 && maxSal == double.MaxValue)
                return null;
            salary.MinVal = minSal;
            salary.MaxVal = maxSal;
            return salary;
        }

        private static string? GetNameFromUser()
        {
            string name;
            Console.WriteLine("Enter Employee Name to Search: ");
            name = Console.ReadLine().Trim();
            if (string.IsNullOrEmpty(name))
                return null;

            return name;
        }

        private static int? GetIdFromUser()
        {
            int id;
            string userInput = "";
            Console.Write("Enter Employee Id to Search: ");
            userInput = Console.ReadLine();
            while (!int.TryParse(userInput, out id))
            {
                if (string.IsNullOrEmpty(userInput))
                    return null;
                Console.WriteLine("Enter a valid number!");
            }
                
            return id;
        }
    }
    public class Range<T>
    {
        public T? MinVal { get; set; }
        public T? MaxVal { get; set; }
    }
}