using System.ComponentModel.DataAnnotations;
using AirWatch.Domain.Entities;

namespace AirWatch.Application.DTOs;

public class CityRequest
{
    [Required] public Guid CountryId { get; set; }
    [Required][MaxLength(150)] public string Name { get; set; }
    [MaxLength(100)] public string? State { get; set; }
    [Required] public decimal Latitude { get; set; }
    [Required] public decimal Longitude { get; set; }
    public decimal? AltitudeM { get; set; }
    public long? Population { get; set; }
    [Required] public char Status { get; set; }

    public City ToDomain() => new(CountryId, Name, State, Latitude, Longitude, AltitudeM, Population, Status);

}