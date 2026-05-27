using System.ComponentModel.DataAnnotations;
using AirWatch.Domain.Entities;

namespace AirWatch.Application.DTOs;

public class IntegrationLogRequest
{
    public Guid? CityId { get; set; }
    [Required][MaxLength(50)] public string ApiName { get; set; }
    [Required][MaxLength(500)] public string Endpoint { get; set; }
    [Required][MaxLength(6)] public string HttpMethod { get; set; }
    public int? HttpStatus { get; set; }
    public int? RecordsCount { get; set; }
    [Required][MaxLength(20)] public string Result { get; set; }
    [MaxLength(1000)] public string? ErrorMessage { get; set; }
    public int? ResponseMs { get; set; }

    public IntegrationLog ToDomain() => new(CityId, ApiName, Endpoint, HttpMethod, HttpStatus, RecordsCount, Result, ErrorMessage, ResponseMs);
}