namespace AutoParts.Models
{
    public class PartCategory : BaseEntity
    {
        public required string Name { get; set; }

        public ICollection<Part> Parts { get; set; } = [];

        public ICollection<PartCompatibility> PartCompatibilities { get; set; } = [];
    }
}
