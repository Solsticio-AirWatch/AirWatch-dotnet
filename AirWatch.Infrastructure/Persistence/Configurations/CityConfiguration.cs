using AirWatch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirWatch.Infrastructure.Persistence.Configurations;

public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.ToTable("CITY");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasColumnName("id_city").ValueGeneratedNever();

        builder.Property(c => c.CountryId).HasColumnName("id_country").IsRequired();

        builder.Property(c => c.Name)
            .HasColumnName("name")
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(c => c.State)
            .HasColumnName("state")
            .HasMaxLength(100);

        builder.Property(c => c.Latitude)
            .HasColumnName("latitude")
            .HasColumnType("decimal(10,6)")
            .IsRequired();

        builder.Property(c => c.Longitude)
            .HasColumnName("longitude")
            .HasColumnType("decimal(10,6)")
            .IsRequired();

        builder.Property(c => c.AltitudeM)
            .HasColumnName("altitude_m")
            .HasColumnType("decimal(8,2)");

        builder.Property(c => c.Population)
            .HasColumnName("population");

        builder.Property(c => c.Status)
            .HasColumnName("status")
            .HasColumnType("CHAR(1)")
            .IsRequired();

        builder.Property(c => c.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(c => c.UpdatedAt)
            .HasColumnName("updated_at");

        builder.HasIndex(c => new { c.Latitude, c.Longitude })
            .HasDatabaseName("idx_city_lat_lng");

        builder.HasIndex(c => c.CountryId)
            .HasDatabaseName("idx_city_country");
    }
}