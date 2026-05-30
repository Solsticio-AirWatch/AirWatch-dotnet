using AirWatch.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AirWatch.Infrastructure.Persistence;

public class AirWatchContext : DbContext
{
    public AirWatchContext(DbContextOptions<AirWatchContext> options) : base(options) { }

    public DbSet<Country> Countries => Set<Country>();
    public DbSet<City> Cities => Set<City>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Sensor> Sensors => Set<Sensor>();
    public DbSet<AirReading> AirReadings => Set<AirReading>();
    public DbSet<AlertConfig> AlertConfigs => Set<AlertConfig>();
    public DbSet<AlertEvent> AlertEvents => Set<AlertEvent>();
    public DbSet<IntegrationLog> IntegrationLogs => Set<IntegrationLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AirWatchContext).Assembly);
    }
}