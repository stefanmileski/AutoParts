namespace AutoParts.Models
{
    public class Manufacturer : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public ICollection<CarModel> CarModels { get; set; } = [];

        public ICollection<Part> Parts { get; set; } = [];
    }
}
