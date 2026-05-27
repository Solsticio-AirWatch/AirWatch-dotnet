using AirWatch.Domain.Common;

namespace AirWatch.Domain.Entities;

public class Sensor : BaseEntity
{
    public Guid CityId { get; private set; }
    public string Name { get; private set; }
    public string Type { get; private set; }
    public string? Location { get; private set; }
    public decimal? Latitude { get; private set; }
    public decimal? Longitude { get; private set; }
    public string Source { get; private set; }
    public string Status { get; private set; }
    public DateTime InstalledAt { get; private set; }
    public DateTime? LastReadingAt { get; private set; }

    public City City { get; private set; } = null!;

    public ICollection<AirReading> AirReadings { get; private set; } = new List<AirReading>();

    public Sensor(Guid cityId, string name, string type, string? location,
                  decimal? latitude, decimal? longitude, string source, string status)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new InvalidOperationException("O nome do sensor é obrigatório.");
        if (string.IsNullOrWhiteSpace(type))
            throw new InvalidOperationException("O tipo do sensor é obrigatório.");
        if (string.IsNullOrWhiteSpace(source))
            throw new InvalidOperationException("A fonte do sensor é obrigatória.");
        if (string.IsNullOrWhiteSpace(status))
            throw new InvalidOperationException("O status do sensor é obrigatório.");

        CityId = cityId;
        Name = name;
        Type = type;
        Location = location;
        Latitude = latitude;
        Longitude = longitude;
        Source = source;
        Status = status;
        InstalledAt = DateTime.UtcNow;
    }

    public void Update(string name, string type, string? location,
                       decimal? latitude, decimal? longitude, string source, string status)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new InvalidOperationException("O nome do sensor é obrigatório.");
        if (string.IsNullOrWhiteSpace(type))
            throw new InvalidOperationException("O tipo do sensor é obrigatório.");

        Name = name;
        Type = type;
        Location = location;
        Latitude = latitude;
        Longitude = longitude;
        Source = source;
        Status = status;
    }
}