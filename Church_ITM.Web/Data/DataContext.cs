using Church_ITM.Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace Church_ITM.Web.Data
{
     public class DataContext : DbContext
     {
          public DataContext(DbContextOptions<DataContext> options) : base(options)
          {
          }

          public DbSet<District> Districts { get; set; }
          public DbSet<Campus> Campuses { get; set; }
          public DbSet<Church> Churchs { get; set; }

          protected override void OnModelCreating(ModelBuilder modelBuilder)
          {
               base.OnModelCreating(modelBuilder);
               modelBuilder.Entity<District>()
               .HasIndex(t => t.Name)
               .IsUnique();
          
               base.OnModelCreating(modelBuilder);
               modelBuilder.Entity<Campus>()
               .HasIndex(t => t.Name)
               .IsUnique();

               base.OnModelCreating(modelBuilder);
               modelBuilder.Entity<Church>()
               .HasIndex(t => t.Name)
               .IsUnique();
          }
     }
}
