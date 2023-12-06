using System.ComponentModel.DataAnnotations;

namespace Interface.Models
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string TelephoneNumber { get; set; }
        public string Reason { get; set; }


        
    }




}
