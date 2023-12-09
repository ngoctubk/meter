using MeterApp.Models;
using Microsoft.EntityFrameworkCore;

namespace MeterApp
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Stall> Stalls { get; set; }
        public DbSet<WaterMeter> WaterMeters { get; set; }
        public DbSet<GasMeter> GasMeters { get; set; }
        public DbSet<Error> Errors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StallConfiguration());
            modelBuilder.ApplyConfiguration(new WaterMeterConfiguration());
            modelBuilder.ApplyConfiguration(new GasMeterConfiguration());
            modelBuilder.ApplyConfiguration(new ErrorConfiguration());
        }
    }
}
