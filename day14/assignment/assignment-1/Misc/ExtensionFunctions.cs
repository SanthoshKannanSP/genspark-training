using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment_1.Misc
{
    internal static class ExtensionFunctions
    {
        public static bool NameValidationCheck(this string name)
        {
            if (name.All(char.IsLetter) && name.Substring(0,1).All(char.IsUpper))
                return true;
            return false;
        }
    }
}
