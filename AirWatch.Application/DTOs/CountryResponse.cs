using AirWatch.Domain.Entities;

namespace AirWatch.Application.DTOs;

public record CountryResponse(Guid Id, string Name, string IsoCode, string? Continent, DateTime CreatedAt)
{
    public static CountryResponse FromDomain(Country c) => new(c.Id, c.Name, c.IsoCode, c.Continent, c.CreatedAt);
}