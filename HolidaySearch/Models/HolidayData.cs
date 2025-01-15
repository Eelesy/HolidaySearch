namespace HolidaySearch.Models
{
    internal class HolidayData
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required DateTime Arrival_Date { get; set; }
        public required double Price_Per_Night { get; set; }
        public required List<string> Local_Airports { get; set; }
        public required int Nights { get; set; }
    }
}