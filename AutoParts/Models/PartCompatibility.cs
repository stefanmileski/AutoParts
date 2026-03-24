namespace AutoParts.Models
{
    public class PartCompatibility : BaseEntity
    {
        public int PartId { get; set; }

        public required Part Part { get; set; }

        public int CarModelId { get; set; }

        public required CarModel CarModel { get; set; }

        public int PartCategoryId { get; set; }

        public required PartCategory PartCategory { get; set; }
    }
}
