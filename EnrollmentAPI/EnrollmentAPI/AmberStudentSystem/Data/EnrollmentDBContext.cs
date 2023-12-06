using AmberStudentSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace AmberStudentSystem.Data
{
    public class EnrollmentDBContext : DbContext
    {
        public EnrollmentDBContext(DbContextOptions options) : base(options)
        {
        
        }

        public DbSet<Student> Student { get; set; }
        public DbSet<Programs> Programs { get; set; }
        public DbSet<Shirt> Shirt { get; set; } 
        public DbSet<Parish> Parish { get; set; } 
    }
}
