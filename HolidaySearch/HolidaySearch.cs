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

        public List<FlightData> GetFlightsFrom(string searchTerm)
        {
            return Flights.Where(x => x.From == searchTerm).ToList();
        }

        public List<FlightData> GetFlightsTo(string searchTerm)
        {
            return Flights.Where(x => x.To == searchTerm).ToList();
        }

        public List<FlightData> GetFlightsOnDepartureDate(string searchTerm)
        {
            return Flights.Where(x => x.Departure_Date.ToShortDateString() == searchTerm).ToList();
        }

        public List<FlightData> FlightSearch(string from, string to, string dateTime)
        {
            return Flights.Where(x => x.From == from && x.To == to && x.Departure_Date.ToShortDateString() == dateTime).ToList();
        }

        public HolidayData GetHolidayDataWithId(int id)
        {
            return Holidays.FirstOrDefault(x => x.Id == id);
        }

    }
}
