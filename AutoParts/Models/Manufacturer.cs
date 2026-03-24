namespace AutoParts.Models
{
    public class Manufacturer : BaseEntity
    {
        public required string Name { get; set; }

        public ICollection<CarModel> CarModels { get; set; } = [];

        public ICollection<Part> Parts { get; set; } = [];
    }
}
