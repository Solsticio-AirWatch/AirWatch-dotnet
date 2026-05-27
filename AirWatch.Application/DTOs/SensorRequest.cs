using System.ComponentModel.DataAnnotations;
using AirWatch.Domain.Entities;

namespace AirWatch.Application.DTOs;

public class SensorRequest
{
    [Required] public Guid CityId { get; set; }
    [Required][MaxLength(100)] public string Name { get; set; }
    [Required][MaxLength(50)] public string Type { get; set; }
    [MaxLength(200)] public string? Location { get; set; }
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    [Required][MaxLength(30)] public string Source { get; set; }
    [Required][MaxLength(20)] public string Status { get; set; }

    public Sensor ToDomain() => new(CityId, Name, Type, Location, Latitude, Longitude, Source, Status);
}