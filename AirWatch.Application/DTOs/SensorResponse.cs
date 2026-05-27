using AirWatch.Domain.Entities;

namespace AirWatch.Application.DTOs;

public record SensorResponse(Guid Id, Guid CityId, string CityName, string Name, string Type, string? Location, decimal? Latitude, decimal? Longitude, string Source, string Status, DateTime InstalledAt, DateTime? LastReadingAt)
{
    public static SensorResponse FromDomain(Sensor s) => new(s.Id, s.CityId, s.City?.Name ?? string.Empty, s.Name, s.Type, s.Location, s.Latitude, s.Longitude, s.Source, s.Status, s.InstalledAt, s.LastReadingAt);
}