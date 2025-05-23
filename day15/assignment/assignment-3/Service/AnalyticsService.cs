using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using assignment_3.Models;

namespace assignment_3.Service
{
    internal class AnalyticsService
    {
        public AnalyticsService() { }

        public void FindMean(DataFrame data)
        {
            for (int i = 0; i<data.Columns.Count; i++)
            {
                var avg = data[i].Average();
                Console.WriteLine($"{data.Columns[i].Trim()} : {avg}");
            }
        }
    }
}
