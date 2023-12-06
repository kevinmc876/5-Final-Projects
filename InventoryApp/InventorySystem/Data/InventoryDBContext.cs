using Microsoft.EntityFrameworkCore;
using InventorySystem.Models;

namespace InventorySystem.Data
{
    public class InventoryDBContext : DbContext
	{
        public InventoryDBContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Products> Product { get; set; }
        public DbSet<Categories> Category { get; set; }
        public DbSet<Sizes> Size { get; set; }
    }
}
