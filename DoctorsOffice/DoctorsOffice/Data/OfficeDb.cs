using DoctorOffice.Models;
using DoctorOffice.Models;
using DoctorsOffice.Models;
using Microsoft.EntityFrameworkCore;


namespace DoctorOffice.Data
{
    public class OfficeDb: DbContext
    {
       public OfficeDb(DbContextOptions<OfficeDb> options) : base(options) { }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient>  Patients { get; set; }
        public DbSet<VisitMethod> VisitMethods { get; set; }

        public DbSet<Userlog> users { get; set; }



    }
}
