using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Interface.Models
{
    public class Appointment
    {
        public int Id { get; set; }


        // the IDs of Tables

        [ForeignKey("Patient")]
        public int PatientId { get; set; }

        [ForeignKey("VisitMethod")]
        public int VisitMethodId { get; set; }

        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }

        public string Description { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        public DateTime Created { get; set; }



        // Navigation
        [Display(Name = "Patient")]
        public Patient? Patient { get; set; }

        [Display(Name = "Visted Method")]
        public VisitMethod? VisitMethod { get; set; }

        [Display(Name = "Doctor")]
        public Doctor? Doctor { get; set; }

 


    }

}
