using System.ComponentModel.DataAnnotations;
using AirWatch.Domain.Entities;

namespace AirWatch.Application.DTOs;

public class UserRequest
{
    public Guid? CityId { get; set; }
    [Required][MaxLength(150)] public string Name { get; set; }
    [Required][EmailAddress][MaxLength(200)] public string Email { get; set; }
    [Required][MinLength(6)] public string Password { get; set; }
    [Required][MaxLength(20)] public string Role { get; set; }
    [MaxLength(20)] public string? Phone { get; set; }
    [Required] public char IsActive { get; set; }
    [Required] public char NotifyEmail { get; set; }
    [Required] public char NotifyPush { get; set; }

    public User ToDomain(string passwordHash) => new(CityId, Name, Email, passwordHash, Role, Phone, IsActive, NotifyEmail, NotifyPush);
}