using System.ComponentModel.DataAnnotations;
namespace assignment_1.Misc
{
    public class NameValidation : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            string name = value as string ?? "";
            if(string.IsNullOrEmpty(name))
              return false;
            foreach(char c in name)
            {
                if(!char.IsLetter(c) && !char.IsWhiteSpace(c))
                    return false;
            }
            return true;
        }
    }
}