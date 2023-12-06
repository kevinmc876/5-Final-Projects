using System.ComponentModel.DataAnnotations;

namespace Interface.Models
{
    public class Userlog
    {
        [Key]
        public int Id { get; set; }

        public string? Username { get; set; }

        public string? Password { get; set; }
    }
}
