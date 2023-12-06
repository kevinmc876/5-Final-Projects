using System.ComponentModel.DataAnnotations;



namespace DoctorOffice.Models
{
    public class Doctor
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Specialization { get; set; }
        public string Telephone { get; set; }

    }

}
