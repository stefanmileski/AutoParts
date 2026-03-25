namespace AutoParts.Models
{
    public class PartCategory : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public ICollection<Part> Parts { get; set; } = [];

        public ICollection<PartCompatibility> PartCompatibilities { get; set; } = [];
    }
}
