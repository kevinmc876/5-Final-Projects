using System.ComponentModel.DataAnnotations;

namespace AmberStudentSystem.Models
{
    public class Shirt
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
