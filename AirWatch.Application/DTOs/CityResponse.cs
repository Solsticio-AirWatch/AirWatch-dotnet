using AirWatch.Domain.Entities;

namespace AirWatch.Application.DTOs;

public record CityResponse(Guid Id, Guid CountryId, string CountryName, string Name, string? State, decimal Latitude, decimal Longitude, decimal? AltitudeM, long? Population, char Status, DateTime CreatedAt, DateTime? UpdatedAt)
{
    public static CityResponse FromDomain(City c) => new(c.Id, c.CountryId, c.Country?.Name ?? string.Empty, c.Name, c.State, c.Latitude, c.Longitude, c.AltitudeM, c.Population, c.Status, c.CreatedAt, c.UpdatedAt);
}