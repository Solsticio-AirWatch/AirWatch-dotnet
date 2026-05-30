using AirWatch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirWatch.Infrastructure.Persistence.Configurations;

public class AlertConfigConfiguration : IEntityTypeConfiguration<AlertConfig>
{
    public void Configure(EntityTypeBuilder<AlertConfig> builder)
    {
        builder.ToTable("ALERT_CONFIG");

        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).HasColumnName("id_alert_config").ValueGeneratedNever();

        builder.Property(a => a.UserId).HasColumnName("id_user").IsRequired();
        builder.Property(a => a.CityId).HasColumnName("id_city").IsRequired();

        builder.Property(a => a.Pollutant)
            .HasColumnName("pollutant")
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(a => a.Threshold)
            .HasColumnName("threshold")
            .HasColumnType("decimal(8,3)")
            .IsRequired();

        builder.Property(a => a.Operator)
            .HasColumnName("operator")
            .HasColumnType("CHAR(2)")
            .IsRequired();

        builder.Property(a => a.Severity)
            .HasColumnName("severity")
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(a => a.IsActive)
            .HasColumnName("is_active")
            .HasColumnType("CHAR(1)")
            .IsRequired();

        builder.Property(a => a.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(a => a.UpdatedAt)
            .HasColumnName("updated_at");

        builder.HasIndex(a => new { a.UserId, a.CityId, a.Pollutant, a.Operator })
            .IsUnique()
            .HasDatabaseName("uq_alert_config");

        builder.HasOne(a => a.User)
            .WithMany(u => u.AlertConfigs)
            .HasForeignKey(a => a.UserId)
            .HasConstraintName("fk_alert_config_user")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.City)
            .WithMany(c => c.AlertConfigs)
            .HasForeignKey(a => a.CityId)
            .HasConstraintName("fk_alert_config_city")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(a => a.UserId).HasDatabaseName("idx_alert_config_user");
        builder.HasIndex(a => a.CityId).HasDatabaseName("idx_alert_config_city");
    }
}