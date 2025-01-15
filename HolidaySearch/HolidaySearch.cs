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
        private List<FlightData> Flights = new List<FlightData>();
        private List<HolidayData> Holidays = new List<HolidayData>();

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

        public int FlightCount()
        {
            return Flights.Count();
        }

        public int HolidayCount()
        {
            return Holidays.Count();
        }

        public FlightData GetFlightDataWithId(int id)
        {
            return Flights.FirstOrDefault(x => x.Id == id);
        }

        public HolidayData GetHolidayDataWithId(int id)
        {
            return Holidays.FirstOrDefault(x => x.Id == id);
        }
    }
}
