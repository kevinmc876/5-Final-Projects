using Microsoft.EntityFrameworkCore;
using PlantStoreApi.Models;
using System;

namespace PlantStoreApi.Data
{
    public class StoreDbContext : DbContext
    {


            public StoreDbContext(DbContextOptions options) : base(options)
            {

            }

            public DbSet<Plant> Plant { get; set; }

            public DbSet<AdoptionRequest> AdoptionRequests { get; set; }

            public DbSet<Adopter> Adopters { get; set; }
    }


    }

