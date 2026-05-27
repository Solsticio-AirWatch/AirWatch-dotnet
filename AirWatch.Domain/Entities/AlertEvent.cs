using AirWatch.Domain.Common;

namespace AirWatch.Domain.Entities;

public class AlertEvent : BaseEntity
{
    public Guid AlertConfigId { get; private set; }
    public Guid ReadingId { get; private set; }
    public decimal MeasuredValue { get; private set; }
    public string? Message { get; private set; }
    public string Status { get; private set; }
    public char NotificationSent { get; private set; }
    public DateTime EventAt { get; private set; }
    public DateTime? NotifiedAt { get; private set; }

    public AlertConfig AlertConfig { get; private set; } = null!;
    public AirReading AirReading { get; private set; } = null!;

    public AlertEvent(Guid alertConfigId, Guid readingId, decimal measuredValue,
        string? message, string status, char notificationSent, DateTime eventAt)
    {
        if (string.IsNullOrWhiteSpace(status))
            throw new InvalidOperationException("O status do evento é obrigatório.");

        AlertConfigId = alertConfigId;
        ReadingId = readingId;
        MeasuredValue = measuredValue;
        Message = message;
        Status = status;
        NotificationSent = notificationSent;
        EventAt = eventAt;
    }

    public void MarkNotified()
    {
        NotificationSent = 'Y';
        NotifiedAt = DateTime.UtcNow;
    }
}