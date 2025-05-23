using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using assignment_2.Models;

namespace assignment_2.Interfaces
{
    internal interface IFile
    {
        public FileData? Read();
    }
}
