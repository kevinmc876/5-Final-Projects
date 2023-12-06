using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Interface.Models
{
    public class AppointmentVM
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int VisitMethodId { get; set; }
        public int DoctorId { get; set; }

        public string? Description { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        public DateTime Created { get; set; }



        //DropDown
        public List<SelectListItem>? PatientList { get; set; }
        public List<SelectListItem>? SelectedPatientId { get; set; }

        public List<SelectListItem>? Doctorlist { get; set; }
        public List<SelectListItem>? SelectedDoctorid { get; set; }

        public List<SelectListItem>? VisitMethodList { get; set; }
        public List<SelectListItem>? SelectedVistedMethodid { get; set; }




        // Navigation
        [Display(Name = "Patient")]
        public Patient? Patient { get; set; }

        [Display(Name = "Vist Method")]
        public VisitMethod? VisitMethod { get; set; }

        [Display(Name = "Doctor")]
        public Doctor? Doctor { get; set; }

    }

}
