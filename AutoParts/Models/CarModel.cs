namespace AutoParts.Models
{
    public class CarModel : BaseEntity
    {
        public required string Name { get; set; }

        public int ManufacturerId { get; set; }

        public required Manufacturer Manufacturer { get; set; }

        public int YearInProductionSince { get; set; }

        public int? YearInProductionTill { get; set; }
    }
}
