using HolidaySearch.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidaySearch
{
    internal class HolidaySearch
    {
        public List<FlightData> Flights = new List<FlightData>();
        public List<HolidayData> Holidays = new List<HolidayData>();

        public HolidaySearch()
        {
            using (StreamReader r = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + @"wwwroot\data\flights.json"))
            {
                string json = r.ReadToEnd();
                Flights = JsonConvert.DeserializeObject<List<FlightData>>(json);
            }

            using (StreamReader r = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + @"wwwroot\data\holidays.json"))
            {
                string json = r.ReadToEnd();
                Holidays = JsonConvert.DeserializeObject<List<HolidayData>>(json);
            }
        }
    }
}
