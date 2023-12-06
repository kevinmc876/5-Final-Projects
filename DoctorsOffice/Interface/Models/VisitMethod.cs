using System.ComponentModel.DataAnnotations;

namespace Interface.Models
{
    public class VisitMethod
    {
        [Key]
        public int Id { get; set; }
        public string MethodName { get; set; }
    }
}
