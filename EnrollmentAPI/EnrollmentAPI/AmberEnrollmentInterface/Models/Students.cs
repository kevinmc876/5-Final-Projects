namespace AmberEnrollmentInterface.Models
{
    public class Students
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }   
        public string  Telephone { get; set;}

        //IDs
        public int ParishId { get; set; }
        public int ProgramsId { get; set; }
        public int ShirtId { get; set; }

        
        //Navigation Properties
        public Parishes? Parish {  get; set; }
        public Programs? Programs { get; set; }
        public Shirts?  Shirt { get; set; }


    }
}
