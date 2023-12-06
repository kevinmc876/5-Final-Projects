using DoctorOffice.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoctorOffice.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        public int PatientId { get; set; }
        public int VisitMethodId { get; set; }
        public int DoctorId { get; set; }

        public string? Description { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        public DateTime Created { get; set; }

        [ForeignKey("PatientId")]
        public virtual Patient? Patient { get; set; }    

        [ForeignKey("VisitMethodId")]
        public virtual VisitMethod? VisitMethod { get; set; }

        [ForeignKey("DoctorId")]
        public virtual Doctor? Doctor { get; set; }
    }

}
