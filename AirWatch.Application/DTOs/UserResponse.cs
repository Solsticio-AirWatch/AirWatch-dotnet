using AirWatch.Domain.Entities;

namespace AirWatch.Application.DTOs;

public record UserResponse(Guid Id, Guid? CityId, string? CityName, string Name, string Email, string Role, string? Phone, char IsActive, char NotifyEmail, char NotifyPush, DateTime CreatedAt, DateTime? LastLoginAt)
{
    public static UserResponse FromDomain(User u) => new(u.Id, u.CityId, u.City?.Name, u.Name, u.Email, u.Role, u.Phone, u.IsActive, u.NotifyEmail, u.NotifyPush, u.CreatedAt, u.LastLoginAt);
}