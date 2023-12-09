using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace SyncJob.Models
{
    public class Stall
    {
        public Guid Id { get; set; }
        public string StallCode { get; set; }
        public string? Name { get; set; }
        public bool UseWaterMeter { get; set; }
        public bool UseGasMeter { get; set; }
        public Guid? LastWaterMeterId { get; set; }
        public DateTime? LastWaterMeterDate { get; set; }
        public double? LastWaterMeter { get; set; }
        public Guid? LatestWaterMeterId { get; set; }
        public DateTime? LatestWaterMeterDate { get; set; }
        public double? LatestWaterMeter { get; set; }
        public Guid? LastGasMeterId { get; set; }
        public DateTime? LastGasMeterDate { get; set; }
        public double? LastGasMeter { get; set; }
        public Guid? LatestGasMeterId { get; set; }
        public DateTime? LatestGasMeterDate { get; set; }
        public double? LatestGasMeter { get; set; }
    }

    public class StallConfiguration : IEntityTypeConfiguration<Stall>
    {
        public void Configure(EntityTypeBuilder<Stall> builder)
        {
            builder.ToTable("Stalls");

            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();

            builder.Property(a => a.StallCode).IsRequired();
            builder.Property(a => a.StallCode).HasMaxLength(40);
            builder.Property(a => a.Name).HasMaxLength(200);

            builder.HasIndex(a => a.StallCode);
            builder.HasIndex(a => a.Name);
        }
    }
}
