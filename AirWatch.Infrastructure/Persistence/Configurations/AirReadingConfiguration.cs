using AirWatch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirWatch.Infrastructure.Persistence.Configurations;

public class AirReadingConfiguration : IEntityTypeConfiguration<AirReading>
{
    public void Configure(EntityTypeBuilder<AirReading> builder)
    {
        builder.ToTable("AIR_READING");

        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).HasColumnName("id_reading").ValueGeneratedNever();

        builder.Property(r => r.CityId).HasColumnName("id_city").IsRequired();
        builder.Property(r => r.SensorId).HasColumnName("id_sensor");

        builder.Property(r => r.Pm25).HasColumnName("pm25").HasColumnType("decimal(8,3)");
        builder.Property(r => r.Pm10).HasColumnName("pm10").HasColumnType("decimal(8,3)");
        builder.Property(r => r.Co2).HasColumnName("co2").HasColumnType("decimal(8,3)");
        builder.Property(r => r.Co).HasColumnName("co").HasColumnType("decimal(8,3)");
        builder.Property(r => r.No2).HasColumnName("no2").HasColumnType("decimal(8,3)");
        builder.Property(r => r.So2).HasColumnName("so2").HasColumnType("decimal(8,3)");
        builder.Property(r => r.O3).HasColumnName("o3").HasColumnType("decimal(8,3)");
        builder.Property(r => r.Temperature).HasColumnName("temperature").HasColumnType("decimal(6,2)");
        builder.Property(r => r.Humidity).HasColumnName("humidity").HasColumnType("decimal(5,2)");
        builder.Property(r => r.Aqi).HasColumnName("aqi");

        builder.Property(r => r.Category)
            .HasColumnName("category")
            .HasMaxLength(20);

        builder.Property(r => r.Source)
            .HasColumnName("source")
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(r => r.ReadingAt)
            .HasColumnName("reading_at")
            .IsRequired();

        builder.HasOne(r => r.City)
            .WithMany(c => c.AirReadings)
            .HasForeignKey(r => r.CityId)
            .HasConstraintName("fk_air_reading_city")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.Sensor)
            .WithMany(s => s.AirReadings)
            .HasForeignKey(r => r.SensorId)
            .HasConstraintName("fk_air_reading_sensor")
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(r => new { r.CityId, r.ReadingAt })
            .HasDatabaseName("idx_air_reading_city_date");

        builder.HasIndex(r => r.ReadingAt)
            .HasDatabaseName("idx_air_reading_date");

        builder.HasIndex(r => r.SensorId)
            .HasDatabaseName("idx_air_reading_sensor");

        builder.HasIndex(r => r.Category)
            .HasDatabaseName("idx_air_reading_category");
    }
}