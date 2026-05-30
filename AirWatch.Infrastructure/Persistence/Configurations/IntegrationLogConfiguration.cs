using AirWatch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirWatch.Infrastructure.Persistence.Configurations;

public class IntegrationLogConfiguration : IEntityTypeConfiguration<IntegrationLog>
{
    public void Configure(EntityTypeBuilder<IntegrationLog> builder)
    {
        builder.ToTable("INTEGRATION_LOG");

        builder.HasKey(l => l.Id);
        builder.Property(l => l.Id).HasColumnName("id_log").ValueGeneratedNever();

        builder.Property(l => l.CityId).HasColumnName("id_city");

        builder.Property(l => l.ApiName)
            .HasColumnName("api_name")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(l => l.Endpoint)
            .HasColumnName("endpoint")
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(l => l.HttpMethod)
            .HasColumnName("http_method")
            .HasColumnType("CHAR(6)")
            .IsRequired();

        builder.Property(l => l.HttpStatus)
            .HasColumnName("http_status");

        builder.Property(l => l.RecordsCount)
            .HasColumnName("records_count");

        builder.Property(l => l.Result)
            .HasColumnName("result")
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(l => l.ErrorMessage)
            .HasColumnName("error_message")
            .HasMaxLength(1000);

        builder.Property(l => l.RequestedAt)
            .HasColumnName("requested_at")
            .IsRequired();

        builder.Property(l => l.ResponseMs)
            .HasColumnName("response_ms");

        builder.HasOne(l => l.City)
            .WithMany(c => c.IntegrationLogs)
            .HasForeignKey(l => l.CityId)
            .HasConstraintName("fk_integration_log_city")
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(l => l.CityId).HasDatabaseName("idx_integration_log_city");
        builder.HasIndex(l => new { l.ApiName, l.RequestedAt }).HasDatabaseName("idx_integration_log_api");
        builder.HasIndex(l => l.Result).HasDatabaseName("idx_integration_log_result");
    }
}