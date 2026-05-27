using System.ComponentModel.DataAnnotations;
using AirWatch.Domain.Entities;

namespace AirWatch.Application.DTOs;

public class AlertEventRequest
{
    [Required] public Guid AlertConfigId { get; set; }
    [Required] public Guid ReadingId { get; set; }
    [Required] public decimal MeasuredValue { get; set; }
    [MaxLength(500)] public string? Message { get; set; }
    [Required][MaxLength(20)] public string Status { get; set; }
    [Required] public char NotificationSent { get; set; }
    [Required] public DateTime EventAt { get; set; }

    public AlertEvent ToDomain() => new(AlertConfigId, ReadingId, MeasuredValue, Message, Status, NotificationSent, EventAt);
}