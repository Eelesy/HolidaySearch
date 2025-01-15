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
            Assert.AreEqual(hs.FlightCount(), 12);
        }

        /// test to show data conversion for holidays is working
        [TestMethod]
        public void ShouldGetAListOf13Holidays()
        {
            Assert.AreEqual(hs.HolidayCount(), 13);
        }

        /// tests to validate model data is correct, and not empty (1 case tried for brevity)
        [TestMethod]
        public void ShouldContainCorrectFlightDetailsForFlightOne()
        {
            var flightOne = hs.GetFlightDataWithId(1);
            Assert.AreEqual(flightOne.Id, 1);
            Assert.AreEqual(flightOne.Airline, "First Class Air");
            Assert.AreEqual(flightOne.From, "MAN");
            Assert.AreEqual(flightOne.To, "TFS");
            Assert.AreEqual(flightOne.Departure_Date.ToShortDateString(), "01/07/2023");
        }

        [TestMethod]
        public void ShouldContainCorrectHolidayDetailsForHolidayOne()
        {
            var holidayOne = hs.GetHolidayDataWithId(1);
            Assert.AreEqual (holidayOne.Id, 1);
            Assert.AreEqual(holidayOne.Name, "Iberostar Grand Portals Nous");
            Assert.AreEqual(holidayOne.Arrival_Date.ToShortDateString(), "05/11/2022");
            Assert.AreEqual(holidayOne.Price_Per_Night, 100.0);
            Assert.AreEqual(holidayOne.Local_Airports[0], "TFS");
            Assert.AreEqual(holidayOne.Nights, 7);
        }

        /// Tests to check that null is returned for a bad id
        [TestMethod]
        public void ShouldReturnNullWhenFlightIdAboveCount()
        {
            var flight = hs.GetFlightDataWithId(13);
            Assert.IsNull (flight);
        }

        [TestMethod]
        public void ShouldReturnNullWhenHolidayIdAbove()
        {
            var holiday = hs.GetHolidayDataWithId(14);
            Assert.IsNull (holiday);    
        }

    }
}
