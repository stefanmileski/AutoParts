namespace AutoParts.Models
{
    public class Part : BaseEntity
    {
        public int PartCategoryId { get; set; }

        public PartCategory PartCategory { get; set; } = null!;

        public int ManufacturerId { get; set; }

        public Manufacturer Manufacturer { get; set; } = null!;

        public string Name { get; set; } = string.Empty;
        
        public string? Description { get; set; }

        public string OemNumber { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int StockQuantity { get; set; }
    }
}
