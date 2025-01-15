using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidaySearch.Models
{
    internal class HolidayData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Arrival_Date { get; set; }
        public double Price_Per_Night { get; set; }
        public List<string> Local_Airports { get; set; }
        public int Nights { get; set; }
    }
}