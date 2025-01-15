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

    }
}
