using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AmberEnrollmentInterface.Models
{
    public class StudentsVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }

        //IDs
        public int ParishsId { get; set; }
        public int ProgramsId { get; set; }
        public int ShirtsId { get; set; }

        //DropDowns
        public List<SelectListItem>? ParishList { get; set; }
        public List<SelectListItem>? ProgramsList { get; set; }
        public List<SelectListItem>? ShirtsList { get; set; }


        //Selected List Items
        [Display(Name = "Parish")]
        public int SelectedParishId { get; set; }

        [Display(Name = "Program")]
        public int SelectedProgramsId { get; set; }

        [Display(Name = "Shirt Size")]
        public int SelectedShirtId { get; set; }

    }
}
