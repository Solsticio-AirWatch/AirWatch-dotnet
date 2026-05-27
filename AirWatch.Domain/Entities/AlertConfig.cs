using AirWatch.Domain.Common;

namespace AirWatch.Domain.Entities;

public class AlertConfig : BaseEntity
{
    public Guid UserId { get; private set; }
    public Guid CityId { get; private set; }
    public string Pollutant { get; private set; }
    public decimal Threshold { get; private set; }
    public string Operator { get; private set; }
    public string Severity { get; private set; }
    public char IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public User User { get; private set; } = null!;
    public City City { get; private set; } = null!;

    public ICollection<AlertEvent> AlertEvents { get; private set; } = new List<AlertEvent>();

    public AlertConfig(Guid userId, Guid cityId, string pollutant, decimal threshold,
                       string @operator, string severity, char isActive)
    {
        if (string.IsNullOrWhiteSpace(pollutant))
            throw new InvalidOperationException("O poluente é obrigatório.");
        if (string.IsNullOrWhiteSpace(@operator))
            throw new InvalidOperationException("O operador é obrigatório.");
        if (string.IsNullOrWhiteSpace(severity))
            throw new InvalidOperationException("A severidade é obrigatória.");

        UserId = userId;
        CityId = cityId;
        Pollutant = pollutant;
        Threshold = threshold;
        Operator = @operator;
        Severity = severity;
        IsActive = isActive;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string pollutant, decimal threshold, string @operator,
                       string severity, char isActive)
    {
        if (string.IsNullOrWhiteSpace(pollutant))
            throw new InvalidOperationException("O poluente é obrigatório.");
        if (string.IsNullOrWhiteSpace(@operator))
            throw new InvalidOperationException("O operador é obrigatório.");

        Pollutant = pollutant;
        Threshold = threshold;
        Operator = @operator;
        Severity = severity;
        IsActive = isActive;
        UpdatedAt = DateTime.UtcNow;
    }
}