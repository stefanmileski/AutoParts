namespace AutoParts.Models
{
    public class Part
    {
        public int Id { get; set; }
       
        public required PartCategory PartCategory { get; set; }
            
        public required int Name { get; set; }
        
        public string? Description { get; set; }

        public required int OemNumber { get; set; }

        public required int Price { get; set; }
    }
}
