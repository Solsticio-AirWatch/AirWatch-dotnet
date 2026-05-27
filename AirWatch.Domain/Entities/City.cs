using AirWatch.Domain.Common;

namespace AirWatch.Domain.Entities;

public class City : BaseEntity
{
    public Guid CountryId { get; private set; }
    public string Name { get; private set; }
    public string? State { get; private set; }
    public decimal Latitude { get; private set; }
    public decimal Longitude { get; private set; }
    public decimal? AltitudeM { get; private set; }
    public long? Population { get; private set; }
    public char Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public Country Country { get; private set; } = null!;

    public ICollection<Sensor> Sensors { get; private set; } = new List<Sensor>();
    public ICollection<AirReading> AirReadings { get; private set; } = new List<AirReading>();
    public ICollection<AlertConfig> AlertConfigs { get; private set; } = new List<AlertConfig>();
    public ICollection<User> Users { get; private set; } = new List<User>();
    public ICollection<IntegrationLog> IntegrationLogs { get; private set; } = new List<IntegrationLog>();

    public City(Guid countryId, string name, string? state,
                decimal latitude, decimal longitude,
                decimal? altitudeM, long? population, char status)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new InvalidOperationException("O nome da cidade é obrigatório.");

        CountryId = countryId;
        Name = name;
        State = state;
        Latitude = latitude;
        Longitude = longitude;
        AltitudeM = altitudeM;
        Population = population;
        Status = status;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string name, string? state, decimal latitude, decimal longitude,
                       decimal? altitudeM, long? population, char status)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new InvalidOperationException("O nome da cidade é obrigatório.");

        Name = name;
        State = state;
        Latitude = latitude;
        Longitude = longitude;
        AltitudeM = altitudeM;
        Population = population;
        Status = status;
        UpdatedAt = DateTime.UtcNow;
    }
}