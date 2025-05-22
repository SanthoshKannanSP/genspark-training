using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment_1.Models
{
    internal class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public string ToString()
        {
            return $"Id: {Id}\nName: {Name}\nAge: {Age}";
        }
    }
}
