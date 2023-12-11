using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlantStoreApi.Models
{
    public class AdoptionRequest
    {
        // --------------------------------------------------------------------------------------------------------------------

        [Key]
        public int ID { get; set; }

        public int PlantID { get; set; }
    
        public int AdopterID { get; set; }

        public DateTime RequestDate { get; set; }
        public string? Status { get; set; }
        public string? Message { get; set; }

        public bool? ApprovalStatus { get; set; }

        // --------------------------------------------------------------------------------------------------------------------

        // Foreign key for User

        [ForeignKey("AdopterID")]
        public Adopter? Adopter { get; set; }

        // Foreign key for Plant


        [ForeignKey("PlantID")]
        public Plant? Plant { get; set; }

        // --------------------------------------------------------------------------------------------------------------------
    }

}