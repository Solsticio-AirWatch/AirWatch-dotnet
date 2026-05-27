using AirWatch.Domain.Entities;

namespace AirWatch.Application.DTOs;

public record AirReadingResponse(Guid Id, Guid CityId, string CityName, Guid? SensorId, string? SensorName, decimal? Pm25, decimal? Pm10, decimal? Co2, decimal? Co, decimal? No2, decimal? So2, decimal? O3, decimal? Temperature, decimal? Humidity, int? Aqi, string? Category, string Source, DateTime ReadingAt)
{
    public static AirReadingResponse FromDomain(AirReading r) => new(r.Id, r.CityId, r.City?.Name ?? string.Empty, r.SensorId, r.Sensor?.Name, r.Pm25, r.Pm10, r.Co2, r.Co, r.No2, r.So2, r.O3, r.Temperature, r.Humidity, r.Aqi, r.Category, r.Source, r.ReadingAt);
}