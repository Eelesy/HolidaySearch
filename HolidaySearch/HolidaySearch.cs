﻿using HolidaySearch.Models;
using Newtonsoft.Json;

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

        #region data accessors

        public int FlightCount()
        {
            return Flights.Count();
        }

        public int HolidayCount()
        {
            return Holidays.Count();
        }

        #endregion

        #region flight search

        public FlightData GetFlightDataWithId(int id)
        {
            return Flights.FirstOrDefault(x => x.Id == id);
        }

        public List<FlightData> GetFlightsFrom(string searchTerm)
        {
            var splitTerm = searchTerm.Split(" ");
            //split the term, if any either return all or the two london, else specific airport
            if (splitTerm[0] == "Any")
            {
                if(splitTerm.Length == 2)
                {
                    return Flights.ToList();
                }
                else
                {
                    return Flights.Where(x => x.From == "LTN" || x.From == "LGW").ToList();
                }
            }
            else
            {
                return Flights.Where(x => x.From == ConvertLocationToAirportCode(splitTerm[0])).ToList();
            }            
        }

        public List<FlightData> GetFlightsTo(string searchTerm)
        {
            var splitTerm = searchTerm.Split(" ");
            return Flights.Where(x => x.To == ConvertLocationToAirportCode(splitTerm[0])).ToList();
        }

        public List<FlightData> GetFlightsOnDepartureDate(string searchTerm)
        {
            DateTime dt = DateTime.Parse(searchTerm);
            string formatted = dt.ToString("yyyy'-'MM'-'dd");
            return Flights.Where(x => x.Departure_Date.ToString("yyyy'-'MM'-'dd") == formatted).ToList();
        }

        public List<FlightData> FlightSearch(string from, string to, string dateTime)
        {
            var flightsFrom = GetFlightsFrom(from);
            var flightsTo = GetFlightsTo(to);
            var flightsOn = GetFlightsOnDepartureDate(dateTime);
            var flightsFromAndTo = flightsFrom.Intersect(flightsTo);
            return flightsFromAndTo.Intersect(flightsOn).ToList();
        }

        #endregion

        #region holiday search

        public HolidayData GetHolidayDataWithId(int id)
        {
            return Holidays.FirstOrDefault(x => x.Id == id);
        }

        public List<HolidayData> GetHolidayToAirport(string searchTerm)
        {
            var splitTerm = searchTerm.Split(" ");
            return Holidays.Where(x => x.Local_Airports.Contains(ConvertLocationToAirportCode(splitTerm[0]))).ToList();
        }

        public List<HolidayData> GetHolidayOnDate(string searchTerm)
        {
            DateTime dt = DateTime.Parse(searchTerm);
            string formatted = dt.ToString("yyyy'-'MM'-'dd");
            return Holidays.Where(x => x.Arrival_Date.ToString("yyyy'-'MM'-'dd") == formatted).ToList();
        }

        public List<HolidayData> GetHolidayForDuration(int duration)
        {
            return Holidays.Where(x => x.Nights == duration).ToList();
        }

        public List<HolidayData> HolidaySearcher(string to, string date, int duration)
        {
            var holidaysTo = GetHolidayToAirport(to);
            var holidaysOn = GetHolidayOnDate(date);
            var holidaysFor = GetHolidayForDuration(duration);

            return holidaysTo.Intersect(holidaysOn).Intersect(holidaysFor).ToList();
        }

        #endregion

        public Result HolidayAndFlightSearcher(string from, string to, string date, int duration, bool findBest = true)
        {
            //destination and date are where the two intersect
            var foundHolidays = HolidaySearcher(to, date, duration);
            var foundFlights = FlightSearch(from, to, date);
            var results = new List<Result>();

            foreach(var holiday in foundHolidays)
            {
                var priceOfHoliday = holiday.Price_Per_Night * duration;
                foreach (var flight in foundFlights)
                {
                    var total = priceOfHoliday + flight.Price;
                    var r = new Result { holiday = holiday, flight = flight, TotalPrice = total };
                    results.Add(r);
                }
            }

            if(results.Count > 0)
            {
                return findBest ? results.OrderBy(x => x.TotalPrice).First() : results[0];
            }
            else
            {
                return null;
            }
        }

        private static string ConvertLocationToAirportCode(string location)
        {
            switch (location)
            {
                case "Manchester":
                case "MAN":
                    return "MAN";
                case "Malaga":
                case "AGP":
                    return "AGP";
                case "London Heathrow":
                case "LTN":
                    return "LTN";
                case "London Gatwick":
                case "LGW":
                    return "LGW";
                case "Mallorca":
                case "PMI":
                    return "PMI";
                case "Gran":
                case "LPA":
                    return "LPA";
                default:
                    return location;
            }
        }

    }
}
