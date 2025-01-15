using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidaySearch.Models
{
    internal class Result
    {
        public HolidayData holiday { get; set; }
        public FlightData flight { get; set; }
        public double TotalPrice { get; set; }
    }
}
