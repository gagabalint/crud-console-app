using Microsoft.EntityFrameworkCore;
using RTKQ6M_HSZF_2024251.Model;

namespace RTKQ6M_HSZF_2024251.Persistence.MsSql
{
    public class RailwayContext : DbContext
    {
        public DbSet<RailwayLine> Railways { get; set; }
        public DbSet<Service> Services { get; set; }

        public RailwayContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=jovonat;Integrated Security=True;MultipleActiveResultSets=true").UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RailwayLine>()
                .HasMany(e => e.Services)
                .WithOne()
                .HasForeignKey(e => e.LineNumber)
                .OnDelete(DeleteBehavior.Cascade);

          
        }
    }
}


