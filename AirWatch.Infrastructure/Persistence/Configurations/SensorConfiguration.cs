using AirWatch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirWatch.Infrastructure.Persistence.Configurations;

public class SensorConfiguration : IEntityTypeConfiguration<Sensor>
{
    public void Configure(EntityTypeBuilder<Sensor> builder)
    {
        builder.ToTable("SENSOR");

        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).HasColumnName("id_sensor").ValueGeneratedNever();

        builder.Property(s => s.CityId).HasColumnName("id_city").IsRequired();

        builder.Property(s => s.Name)
            .HasColumnName("name")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(s => s.Type)
            .HasColumnName("type")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(s => s.Location)
            .HasColumnName("location")
            .HasMaxLength(200);

        builder.Property(s => s.Latitude)
            .HasColumnName("latitude")
            .HasColumnType("decimal(10,6)");

        builder.Property(s => s.Longitude)
            .HasColumnName("longitude")
            .HasColumnType("decimal(10,6)");

        builder.Property(s => s.Source)
            .HasColumnName("source")
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(s => s.Status)
            .HasColumnName("status")
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(s => s.InstalledAt)
            .HasColumnName("installed_at")
            .IsRequired();

        builder.Property(s => s.LastReadingAt)
            .HasColumnName("last_reading_at");

        builder.HasOne(s => s.City)
            .WithMany(c => c.Sensors)
            .HasForeignKey(s => s.CityId)
            .HasConstraintName("fk_sensor_city")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(s => s.CityId)
            .HasDatabaseName("idx_sensor_city");

        builder.HasIndex(s => s.Status)
            .HasDatabaseName("idx_sensor_status");
    }
}