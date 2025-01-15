namespace HolidaySearch.Models
{
    internal class Result
    {
        public required HolidayData holiday { get; set; }
        public required FlightData flight { get; set; }
        public required double TotalPrice { get; set; }
    }
}
