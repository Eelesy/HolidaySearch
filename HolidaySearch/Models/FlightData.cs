namespace HolidaySearch.Models
{
    internal class FlightData
    {
        public int Id { get; set; }
        public required string Airline { get; set; }
        public required string From { get; set; }
        public required string To { get; set; }
        public required double Price { get; set; }
        public required DateTime Departure_Date { get; set; }
    }
}
