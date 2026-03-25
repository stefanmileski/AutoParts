namespace AutoParts.Models
{
    public class CarModel : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public int ManufacturerId { get; set; }

        public Manufacturer Manufacturer { get; set; } = null!;

        public int YearInProductionSince { get; set; }

        public int? YearInProductionTill { get; set; }
    }
}
