using AirWatch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirWatch.Infrastructure.Persistence.Configurations;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.ToTable("COUNTRY");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasColumnName("id_country").ValueGeneratedNever();

        builder.Property(c => c.Name)
            .HasColumnName("name")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.IsoCode)
            .HasColumnName("iso_code")
            .HasColumnType("CHAR(2)")
            .IsRequired();

        builder.HasIndex(c => c.IsoCode)
            .IsUnique()
            .HasDatabaseName("uq_country_iso");

        builder.Property(c => c.Continent)
            .HasColumnName("continent")
            .HasMaxLength(50);

        builder.Property(c => c.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.HasMany(c => c.Cities)
            .WithOne(ct => ct.Country)
            .HasForeignKey(ct => ct.CountryId)
            .HasConstraintName("fk_city_country")
            .OnDelete(DeleteBehavior.Restrict);
    }
}