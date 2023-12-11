using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlantInterface.Areas.Identity.Data;
using PlantInterface.Models;

namespace PlantInterface.Data;

public class PlantDbContext : IdentityDbContext<PlantUser>
{
    public PlantDbContext(DbContextOptions<PlantDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

    public DbSet<PlantInterface.Models.Plant> Plant { get; set; } = default!;
}
