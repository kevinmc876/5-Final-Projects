using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmberStudentSystem.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Telephone {  get; set; }

        //ForeignKeys IDs
        public int ParishId { get; set; }
        public int ProgramsId { get; set; }
        public int ShirtId { get; set; }


        //Navigation 
        [ForeignKey("ParishId")]
        public virtual Parish? Parish { get; set; }
        
        [ForeignKey("ProgramsId")]
        public virtual Programs? Programs { get; set; }
        
        [ForeignKey("ShirtId")]
        public virtual Shirt? Shirt { get; set; }

    }
}
