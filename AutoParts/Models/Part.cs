namespace AutoParts.Models
{
    public class Part : BaseEntity
    {
        public int PartCategoryId { get; set; }

        public required PartCategory PartCategory { get; set; }

        public int ManufacturerId { get; set; }

        public required Manufacturer Manufacturer { get; set; }

        public required string Name { get; set; }
        
        public string? Description { get; set; }

        public required string OemNumber { get; set; }

        public required decimal Price { get; set; }

        public required int StockQuantity { get; set; }
    }
}
