using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Interface.Models
{
    public class AdoptionRequestVM
    {
        // --------------------------------------------------------------------------------------------------------------------
        public int ID { get; set; }

        public int PlantID { get; set; }
    
        public int AdopterID { get; set; }

        public DateTime RequestDate { get; set; }
        public string? Status { get; set; }
        public string? Message { get; set; }

        public bool? ApprovalStatus { get; set; }

        // --------------------------------------------------------------------------------------------------------------------

        public Adopter? Adopter { get; set; }
        public Plant? Plant { get; set; }

        // --------------------------------------------------------------------------------------------------------------------


        //DropDown
        [NotMapped]
        public List<SelectListItem>? Plantlist { get; set; }
        public int SelectedPlantID { get; set; }
        [NotMapped]
        public List<SelectListItem>? Adopterlist { get; set; }
        public int SelectedAdopterID { get; set; }
    }

}