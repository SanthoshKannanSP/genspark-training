using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment_3.Models
{
    internal class DataFrameAdapter : DataFrame
    {
        private string _csv = string.Empty;
        public DataFrameAdapter(string csv)
        {
            this._csv = csv;
        }

        public int[] this[int column] => _csv.Split("\n")[1..].Select(row => row.Split(",")[column]).Select(int.Parse).ToArray();

        public List<string> Columns
        { 
            get => _csv.Split("\n")[0].Split(",").ToList(); 
        }
    }
}
