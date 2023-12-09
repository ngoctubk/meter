using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace MeterApp.Models
{
    public class WaterMeter
    {
        public Guid Id { get; set; }
        public string StallCode { get; set; }
        public int Cycle { get; set; }
        public double Value { get; set; }
        public double Raw {  get; set; }
        public double Pre {  get; set; }
        public string Error {  get; set; }
        public string Rate { get; set; }
        public DateTime FromTimestamp { get; set; }
        public DateTime ToTimestamp { get; set; }
    }

    public class WaterMeterConfiguration : IEntityTypeConfiguration<WaterMeter>
    {
        public void Configure(EntityTypeBuilder<WaterMeter> builder)
        {
            builder.ToTable("WaterMeters");

            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();

            builder.Property(a => a.StallCode).IsRequired();

            builder.Property(a => a.StallCode).HasMaxLength(40);
            builder.Property(a => a.Value).HasMaxLength(40);
            builder.Property(a => a.Raw).HasMaxLength(40);
            builder.Property(a => a.Pre).HasMaxLength(40);
            builder.Property(a => a.Rate).HasMaxLength(40);

            builder.HasIndex(a => a.StallCode);
            builder.HasIndex(a => a.FromTimestamp);
            builder.HasIndex(a => a.ToTimestamp);
            builder.HasIndex(a => a.Cycle);
            builder.HasIndex(a => a.Value);
        }
    }
}
