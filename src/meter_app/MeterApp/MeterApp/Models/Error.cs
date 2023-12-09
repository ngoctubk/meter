using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace MeterApp.Models
{
    public class Error
    {
        public Guid Id { get; set; }
        public string Topic { get; set; }
        public string StallCode { get; set; }
        public string Payload { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class ErrorConfiguration : IEntityTypeConfiguration<Error>
    {
        public void Configure(EntityTypeBuilder<Error> builder)
        {
            builder.ToTable("Errors");

            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();

            builder.Property(a => a.StallCode).IsRequired();

            builder.Property(a => a.Topic).HasMaxLength(100);
            builder.Property(a => a.StallCode).HasMaxLength(40);

            builder.HasIndex(a => a.StallCode);
            builder.HasIndex(a => a.Timestamp);

        }
    }
}
