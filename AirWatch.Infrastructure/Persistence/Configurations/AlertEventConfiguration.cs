using AirWatch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirWatch.Infrastructure.Persistence.Configurations;

public class AlertEventConfiguration : IEntityTypeConfiguration<AlertEvent>
{
   public void Configure(EntityTypeBuilder<AlertEvent> builder)
    {
        builder.ToTable("ALERT_EVENT");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id_event").ValueGeneratedNever();

        builder.Property(e => e.AlertConfigId).HasColumnName("id_alert_config").IsRequired();
        builder.Property(e => e.ReadingId).HasColumnName("id_reading").IsRequired();

        builder.Property(e => e.MeasuredValue)
            .HasColumnName("measured_value")
            .HasColumnType("decimal(8,3)")
            .IsRequired();

        builder.Property(e => e.Message)
            .HasColumnName("message")
            .HasMaxLength(500);

        builder.Property(e => e.Status)
            .HasColumnName("status")
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(e => e.NotificationSent)
            .HasColumnName("notification_sent")
            .HasColumnType("CHAR(1)")
            .IsRequired();

        builder.Property(e => e.EventAt)
            .HasColumnName("event_at")
            .IsRequired();

        builder.Property(e => e.NotifiedAt)
            .HasColumnName("notified_at");

        builder.HasOne(e => e.AlertConfig)
            .WithMany(a => a.AlertEvents)
            .HasForeignKey(e => e.AlertConfigId)
            .HasConstraintName("fk_alert_event_config")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.AirReading)
            .WithMany(r => r.AlertEvents)
            .HasForeignKey(e => e.ReadingId)
            .HasConstraintName("fk_alert_event_reading")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(e => e.AlertConfigId).HasDatabaseName("idx_alert_event_config");
        builder.HasIndex(e => e.ReadingId).HasDatabaseName("idx_alert_event_reading");
        builder.HasIndex(e => e.EventAt).HasDatabaseName("idx_alert_event_date");
        builder.HasIndex(e => e.Status).HasDatabaseName("idx_alert_event_status");
    } 
}