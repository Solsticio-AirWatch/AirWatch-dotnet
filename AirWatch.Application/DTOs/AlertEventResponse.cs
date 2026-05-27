using AirWatch.Domain.Entities;

namespace AirWatch.Application.DTOs;

public record AlertEventResponse(Guid Id, Guid AlertConfigId, string AlertPollutant, string AlertSeverity, Guid ReadingId, decimal MeasuredValue, string? Message, string Status, char NotificationSent, DateTime EventAt, DateTime? NotifiedAt)
{
    public static AlertEventResponse FromDomain(AlertEvent e) => new(e.Id, e.AlertConfigId, e.AlertConfig?.Pollutant ?? string.Empty, e.AlertConfig?.Severity ?? string.Empty, e.ReadingId, e.MeasuredValue, e.Message, e.Status, e.NotificationSent, e.EventAt, e.NotifiedAt);
}