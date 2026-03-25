namespace AutoParts.Models
{
    public class PartCompatibility : BaseEntity
    {
        public int PartId { get; set; }

        public Part Part { get; set; } = null!;

        public int CarModelId { get; set; }

        public CarModel CarModel { get; set; } = null!;

        public int PartCategoryId { get; set; }

        public PartCategory PartCategory { get; set; } = null!;
    }
}
