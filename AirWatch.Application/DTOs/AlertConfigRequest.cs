using System.ComponentModel.DataAnnotations;
using AirWatch.Domain.Entities;

namespace AirWatch.Application.DTOs;

public class AlertConfigRequest
{
    [Required] public Guid UserId { get; set; }
    [Required] public Guid CityId { get; set; }
    [Required][MaxLength(20)] public string Pollutant { get; set; }
    [Required] public decimal Threshold { get; set; }
    [Required][MaxLength(2)] public string Operator { get; set; }
    [Required][MaxLength(20)] public string Severity { get; set; }
    [Required] public char IsActive { get; set; }

    public AlertConfig ToDomain() => new(UserId, CityId, Pollutant, Threshold, Operator, Severity, IsActive);
}