using System.ComponentModel.DataAnnotations;

namespace PlantStoreApi.Models
{
    public class Plant
    {
        [Key]
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Age { get; set; }
        public string? Species { get; set; }
        public string? ImageURL { get; set; }
        public bool AvailabilityStatus { get; set; }
        public bool AdoptionStatus { get; set; }
    }
}
