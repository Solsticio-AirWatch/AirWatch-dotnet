using System.ComponentModel.DataAnnotations;
using AirWatch.Domain.Entities;

namespace AirWatch.Application.DTOs;

public class AirReadingRequest
{
    [Required] public Guid CityId { get; set; }
    public Guid? SensorId { get; set; }
    public decimal? Pm25 { get; set; }
    public decimal? Pm10 { get; set; }
    public decimal? Co2 { get; set; }
    public decimal? Co { get; set; }
    public decimal? No2 { get; set; }
    public decimal? So2 { get; set; }
    public decimal? O3 { get; set; }
    public decimal? Temperature { get; set; }
    public decimal? Humidity { get; set; }
    public int? Aqi { get; set; }
    [MaxLength(20)] public string? Category { get; set; }
    [Required][MaxLength(30)] public string Source { get; set; }
    [Required] public DateTime ReadingAt { get; set; }

    public AirReading ToDomain() => new(CityId, SensorId, Pm25, Pm10, Co2, Co, No2, So2, O3, Temperature, Humidity, Aqi, Category, Source, ReadingAt);
}