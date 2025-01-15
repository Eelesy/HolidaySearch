using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HolidaySearch
{

    // spec - create a holiday search class
    //    var holidaySearch = new HolidaySearch({
    // DepartingFrom: 'MAN',
    // TravelingTo: 'AGP',
    // DepartureDate: '2023/07/01'
    //Duration: 7
    // });
    //holidaySearch.Results.First() # Returns the Best of the matching results
    // holidaySearch.Results.First().TotalPrice # '£900.00'
    // holidaySearch.Results.First().Flight.Id # 4
    // holidaySearch.Results.First().Flight.DepartingFrom # 'MAN'
    // holidaySearch.Results.First().Flight.TravalingTo # 'AGP'
    // holidaySearch.Results.First().Flight.Price
    // holidaySearch.Results.First().Hotel.Id # 3
    // holidaySearch.Results.First().Hotel.Name
    // holidaySearch.Results.First().Hotel.Price
    // Test cases
    // Here are some example test cases
    // #### Customer #1
    // ##### Input
    // * Departing from: Manchester Airport(MAN)
    // *Traveling to: Malaga Airport(AGP)
    // *Departure Date: 2023 / 07 / 01
    // * Duration: 7 nights
    //##### Expected result
    // * Flight 2 and Hotel 9
    // ### Customer #2
    // ##### Input
    //* Departing from: Any London Airport
    // * Traveling to: Mallorca Airport(PMI)
    // *Departure Date: 2023 / 06 / 15
    // * Duration: 10 nights
    //##### Expected result
    // * Flight 6 and Hotel 5
    // ### Customer #3
    // ##### Input
    // * Departing from: Any Airport
    // * Traveling to: Gran Canaria Airport (LPA)
    // * Departure Date: 2022 / 11 / 10
    // * Duration: 14 nights
    //##### Expected result
    // * Flight 7 and Hotel 6

    [TestClass]
    public class HolidaySearchTests
    {
        HolidaySearch hs = new HolidaySearch();
        /// Test to show data conversion for flight is working as expected
        [TestMethod]
        public void ShouldGetAListOf12Flights()
        {
            Assert.AreEqual(12, hs.FlightCount());
        }

        /// test to show data conversion for holidays is working
        [TestMethod]
        public void ShouldGetAListOf13Holidays()
        {
            Assert.AreEqual(13, hs.HolidayCount());
        }

        /// tests to validate model data is correct, and not empty (1 case tried for brevity)
        [TestMethod]
        public void ShouldContainCorrectFlightDetailsForFlightOne()
        {
            var flightOne = hs.GetFlightDataWithId(1);
            Assert.AreEqual(1, flightOne.Id);
            Assert.AreEqual("First Class Air", flightOne.Airline);
            Assert.AreEqual("MAN", flightOne.From);
            Assert.AreEqual("TFS", flightOne.To);
            Assert.AreEqual(470, flightOne.Price);
            Assert.AreEqual("01/07/2023", flightOne.Departure_Date.ToShortDateString());
        }

        [TestMethod]
        public void ShouldContainCorrectHolidayDetailsForHolidayOne()
        {
            var holidayOne = hs.GetHolidayDataWithId(1);
            Assert.AreEqual(1, holidayOne.Id);
            Assert.AreEqual("Iberostar Grand Portals Nous", holidayOne.Name);
            Assert.AreEqual("05/11/2022", holidayOne.Arrival_Date.ToShortDateString());
            Assert.AreEqual(100.00, holidayOne.Price_Per_Night);
            Assert.AreEqual("TFS", holidayOne.Local_Airports[0]);
            Assert.AreEqual(7, holidayOne.Nights);
        }

        /// Tests to check that null is returned for a bad id
        [TestMethod]
        public void ShouldReturnNullWhenFlightIdAboveCount()
        {
            var flight = hs.GetFlightDataWithId(13);
            Assert.IsNull (flight);
        }

        ///Test to search for flight by from, to and date
        [TestMethod]
        public void ShouldReturnNullWhenHolidayIdAbove()
        {
            var holiday = hs.GetHolidayDataWithId(14);
            Assert.IsNull (holiday);    
        }

        [TestMethod]
        public void ShouldReturnFlightsFromADestination()
        {
            var foundFlight = hs.GetFlightsFrom("MAN");
            Assert.AreEqual(8, foundFlight.Count());
        }

        [TestMethod]
        public void ShouldReturnFlightsToADestination()
        {
            var foundFlight = hs.GetFlightsTo("TFS");
            Assert.AreEqual(1, foundFlight.Count());
        }

        [TestMethod]
        public void ShouldReturnFlightsOnADepatureDate()
        {
            var foundFlight = hs.GetFlightsOnDepartureDate("01/07/2023");
            Assert.AreEqual(4, foundFlight.Count);
        }

        // negative test
        [TestMethod]
        public void ShouldReturnEmptyListForNoFlightsWithSearchTerm()
        {
            var flightFrom = hs.GetFlightsFrom("LEEK");
            var flightsTo = hs.GetFlightsTo("WILMSLOW");
            var flightOn = hs.GetFlightsOnDepartureDate("15/01/2025");
            Assert.AreEqual(0, flightFrom.Count());
            Assert.AreEqual(0, flightsTo.Count());
            Assert.AreEqual(0, flightOn.Count());
        }

        // combine the three search terms into one flight function (leave previous methods for thought process)
        [TestMethod]
        public void ShouldReturnFlightsForThreeSearchTerms()
        {
            var flightsFound = hs.FlightSearch("MAN", "TFS", "01/07/2023");
            Assert.AreEqual(1, flightsFound.Count());
        }

        [TestMethod]
        public void ShouldGetEmptyListForNoFlight()
        {
            var flightsFound = hs.FlightSearch("LEEK", "WILMSLOW", "15/01/2024");
            Assert.AreEqual(0, flightsFound.Count());
        }

        //three search terms for the holidays, airport to, date and duration
        //combined test and negative tests
        [TestMethod]
        public void ShouldReturnHolidayToAirport()
        {
            var foundHoliday = hs.GetHolidayToAirport("PMI");
            Assert.AreEqual(4, foundHoliday.Count());
        }

        [TestMethod]
        public void ShouldReturnHolidayOnDate()
        {
            var foundHoliday = hs.GetHolidayOnDate("10/10/2022");
            Assert.AreEqual(1, foundHoliday.Count());
        }

        [TestMethod]
        public void ShouldReturnHolidayForDuration()
        {
            var foundHoliday = hs.GetHolidayForDuration(7);
            Assert.AreEqual(5, foundHoliday.Count());
        }

        // negative test
        [TestMethod]
        public void ShouldReturnEmptyListForNoHolidaysWithSearchTerm()
        {
            var holidayTo = hs.GetHolidayToAirport("WILMSLOW");
            var holidayOn = hs.GetHolidayOnDate("15/01/2025");
            var holidayDuration = hs.GetHolidayForDuration(1);
            Assert.AreEqual(0, holidayTo.Count());
            Assert.AreEqual(0, holidayOn.Count());
            Assert.AreEqual(0, holidayDuration.Count());
        }

        [TestMethod]
        public void ShouldReturnHolidaysForMultipleTerms()
        {
            var foundHolidays = hs.HolidaySearcher("TFS", "05/11/2022", 7);
            Assert.AreEqual(2, foundHolidays.Count());
        }

        [TestMethod]
        public void ShouldReturnEmptyListForNoHolidays()
        {
            var foundHolidays = hs.HolidaySearcher("WILMSLOW", "01/01/2025", 10);
            Assert.AreEqual(0, foundHolidays.Count());
        }

        //before merging the two searches together, looking at the spec there can be some ambiguity in search terms - we can search airport without code, and an Any search term for depature
        // date format also needs to be handled
        //as this is a limited set, create some quick conversions to bake in this ambuiguity
        [TestMethod]
        public void ShouldHandleSearchingForPlaceNameForFlights()
        {
            var manchesterFlights = hs.GetFlightsFrom("Manchester Airport");
            var malagaFlights = hs.GetFlightsTo("Malaga Airport");
            var londonFlights = hs.GetFlightsFrom("Any London Airport");
            var anyFlight = hs.GetFlightsFrom("Any Airport");

            Assert.AreEqual(8, manchesterFlights.Count());
            Assert.AreEqual(5, malagaFlights.Count());
            Assert.AreEqual(4, londonFlights.Count());
            Assert.AreEqual(12, anyFlight.Count());
        }

        [TestMethod]
        public void ShouldHandleAnySearchInFlightSearcher()
        {
            var londonFlights = hs.FlightSearch("Any London Airport", "Mallorca Airport", "15/06/2023");
            Assert.AreEqual(2,  londonFlights.Count());
        }

        [TestMethod]
        public void ShouldHandleDifferentDateFormatsForSearch()
        {
            var dateFormatOne = hs.GetFlightsOnDepartureDate("2023/06/15");
            var dateFormatTwo = hs.GetHolidayOnDate("15/06/2023");
            var dateFormatThree = hs.GetFlightsOnDepartureDate("15-06-2023");
            Assert.AreEqual(4, dateFormatOne.Count());
            Assert.AreEqual(4, dateFormatTwo.Count());
            Assert.AreEqual(4, dateFormatThree.Count());
        }

        //now we can merge the two and return holidays and flights
        //start with a holidays and flights that match and return the first occurence
        // assert the terms not search are what we expect
        [TestMethod]
        public void ShouldFindHolidayAndFlightForSearchTerms()
        {
            var foundHolidayAndFlight = hs.HolidayAndFlightSearcher("MAN", "PMI", "15/06/2023", 10);
            var holiday = foundHolidayAndFlight.Item1;
            var flight = foundHolidayAndFlight.Item2;
            Assert.AreEqual("Sol Katmandu Park & Resort", holiday.Name);
            Assert.AreEqual(60, holiday.Price_Per_Night);
            Assert.AreEqual("Trans American Airlines", flight.Airline);
            Assert.AreEqual(170, flight.Price);
        }
    }
}
