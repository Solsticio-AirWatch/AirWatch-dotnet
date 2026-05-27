using AirWatch.Domain.Common;

namespace AirWatch.Domain.Entities;

public class User : BaseEntity
{
  public Guid? CityId { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public string Role { get; private set; }
    public string? Phone { get; private set; }
    public char IsActive { get; private set; }
    public char NotifyEmail { get; private set; }
    public char NotifyPush { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? LastLoginAt { get; private set; }

    public City? City { get; private set; }

    public ICollection<AlertConfig> AlertConfigs { get; private set; } = new List<AlertConfig>();

    public User(Guid? cityId, string name, string email, string passwordHash,
                string role, string? phone, char isActive, char notifyEmail, char notifyPush)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new InvalidOperationException("O nome é obrigatório.");
        if (string.IsNullOrWhiteSpace(email))
            throw new InvalidOperationException("O e-mail é obrigatório.");
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new InvalidOperationException("A senha é obrigatória.");

        CityId = cityId;
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
        Phone = phone;
        IsActive = isActive;
        NotifyEmail = notifyEmail;
        NotifyPush = notifyPush;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(Guid? cityId, string name, string email, string role,
                       string? phone, char isActive, char notifyEmail, char notifyPush)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new InvalidOperationException("O nome é obrigatório.");
        if (string.IsNullOrWhiteSpace(email))
            throw new InvalidOperationException("O e-mail é obrigatório.");

        CityId = cityId;
        Name = name;
        Email = email;
        Role = role;
        Phone = phone;
        IsActive = isActive;
        NotifyEmail = notifyEmail;
        NotifyPush = notifyPush;
    }  
}