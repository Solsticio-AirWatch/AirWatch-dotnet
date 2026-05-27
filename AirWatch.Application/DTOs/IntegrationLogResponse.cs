using AirWatch.Domain.Entities;

namespace AirWatch.Application.DTOs;

public record IntegrationLogResponse(Guid Id, Guid? CityId, string? CityName, string ApiName, string Endpoint, string HttpMethod, int? HttpStatus, int? RecordsCount, string Result, string? ErrorMessage, DateTime RequestedAt, int? ResponseMs)
{
    public static IntegrationLogResponse FromDomain(IntegrationLog l) => new(l.Id, l.CityId, l.City?.Name, l.ApiName, l.Endpoint, l.HttpMethod, l.HttpStatus, l.RecordsCount, l.Result, l.ErrorMessage, l.RequestedAt, l.ResponseMs);
}