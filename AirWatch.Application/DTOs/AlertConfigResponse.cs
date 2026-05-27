using AirWatch.Domain.Entities;

namespace AirWatch.Application.DTOs;

public record AlertConfigResponse(Guid Id, Guid UserId, string UserName, Guid CityId, string CityName, string Pollutant, decimal Threshold, string Operator, string Severity, char IsActive, DateTime CreatedAt, DateTime? UpdatedAt)
{
    public static AlertConfigResponse FromDomain(AlertConfig a) => new(a.Id, a.UserId, a.User?.Name ?? string.Empty, a.CityId, a.City?.Name ?? string.Empty, a.Pollutant, a.Threshold, a.Operator, a.Severity, a.IsActive, a.CreatedAt, a.UpdatedAt);
}