using Interface.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Interface.Models;
using PlantStoreApi.Models;

namespace Interface.Data;

public class InterfaceDbContext : IdentityDbContext<InterfaceUser>
{
    public InterfaceDbContext(DbContextOptions<InterfaceDbContext> options)
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

    public DbSet<Interface.Models.AdoptionRequest> AdoptionRequest { get; set; } = default!;

    public DbSet<Interface.Models.AdoptionRequestVM> AdoptionRequestVM { get; set; } = default!;

    public DbSet<Interface.Models.Adopter> Adopter { get; set; } = default!;

    public DbSet<Interface.Models.Plant> Plant { get; set; } = default!;

    public DbSet<PlantStoreApi.Models.Adopter> Adopter_1 { get; set; } = default!;

}
