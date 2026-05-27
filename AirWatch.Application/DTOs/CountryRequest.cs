using System.ComponentModel.DataAnnotations;
using AirWatch.Domain.Entities;

namespace AirWatch.Application.DTOs;

public class CountryRequest
{
    [Required][MaxLength(100)] public string Name { get; set; }
    [Required][StringLength(2, MinimumLength = 2)] public string IsoCode { get; set; }
    [MaxLength(50)] public string? Continent { get; set; }
    
    public Country ToDomain() => new(Name, IsoCode, Continent);
}