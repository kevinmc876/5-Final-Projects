using System.ComponentModel.DataAnnotations;

namespace DoctorOffice.Models
{
    public class VisitMethod
    {
        [Key]
        public int Id { get; set; }
        public string MethodName { get; set; }
    }
}
