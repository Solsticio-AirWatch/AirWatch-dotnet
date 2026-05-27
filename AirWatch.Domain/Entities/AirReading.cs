using AirWatch.Domain.Common;

namespace AirWatch.Domain.Entities;

public class AirReading : BaseEntity
{
    public Guid CityId { get; private set; }
    public Guid? SensorId { get; private set; }
    public decimal? Pm25 { get; private set; }
    public decimal? Pm10 { get; private set; }
    public decimal? Co2 { get; private set; }
    public decimal? Co { get; private set; }
    public decimal? No2 { get; private set; }
    public decimal? So2 { get; private set; }
    public decimal? O3 { get; private set; }
    public decimal? Temperature { get; private set; }
    public decimal? Humidity { get; private set; }
    public int? Aqi { get; private set; }
    public string? Category { get; private set; }
    public string Source { get; private set; }
    public DateTime ReadingAt { get; private set; }

    public City City { get; private set; } = null!;
    public Sensor? Sensor { get; private set; }

    public ICollection<AlertEvent> AlertEvents { get; private set; } = new List<AlertEvent>();

    public AirReading(Guid cityId, Guid? sensorId, decimal? pm25, decimal? pm10,
        decimal? co2, decimal? co, decimal? no2, decimal? so2, decimal? o3,
        decimal? temperature, decimal? humidity, int? aqi,
        string? category, string source, DateTime readingAt)
    {
        if (string.IsNullOrWhiteSpace(source))
            throw new InvalidOperationException("A fonte da leitura é obrigatória.");

        CityId = cityId;
        SensorId = sensorId;
        Pm25 = pm25;
        Pm10 = pm10;
        Co2 = co2;
        Co = co;
        No2 = no2;
        So2 = so2;
        O3 = o3;
        Temperature = temperature;
        Humidity = humidity;
        Aqi = aqi;
        Category = category;
        Source = source;
        ReadingAt = readingAt;
    }
}