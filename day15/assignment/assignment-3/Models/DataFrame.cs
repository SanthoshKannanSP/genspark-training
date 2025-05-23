using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment_3.Models
{
    internal interface DataFrame
    {
        public List<string> Columns { get; }
        public int[] this[int column] { get; }
    }
}
