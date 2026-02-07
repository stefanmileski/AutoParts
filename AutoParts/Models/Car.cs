namespace AutoParts.Models
{
    public class Car
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public required Manufacturer Manufacturer { get; set; }

        public required int YearInProductionSince { get; set; }

        public int? YearInProductionTill { get; set; }
    }
}
