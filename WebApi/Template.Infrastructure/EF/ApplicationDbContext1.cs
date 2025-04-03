using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
using Template.Domain.DataModels;

namespace Template.Infrastructure.EF
{
    public partial class ApplicationDbContext1 : DbContext
    {
        public ApplicationDbContext1(DbContextOptions options)
         : base(options)
        {
        }

       

        public virtual DbSet<Auto> Autos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseInMemoryDatabase("MemoryDb");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Auto>().HasData(
                Auto.Create(1, 2020, "Benzin", "Audi A4"),
                Auto.Create(2, 2018, "Dizel", "BMW X5"),
                Auto.Create(3, 2022, "Električni", "Tesla Model 3")
            );

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
