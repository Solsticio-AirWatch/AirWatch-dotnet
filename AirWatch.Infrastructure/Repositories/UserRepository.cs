using AirWatch.Application.DTOs;
using AirWatch.Application.Services;
using AirWatch.Domain.Entities;
using AirWatch.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AirWatch.Infrastructure.Repositories;

public class UserRepository :  IUserRepository
{
    private readonly AirWatchContext _ctx;
    public UserRepository(AirWatchContext ctx) => _ctx = ctx;

    public IReadOnlyList<UserResponse> GetAll() =>
        _ctx.Users.AsNoTracking().Include(u => u.City).OrderBy(u => u.Name)
            .Select(u => UserResponse.FromDomain(u)).ToList();

    public UserResponse? GetById(Guid id)
    {
        var u = _ctx.Users.Include(u => u.City).FirstOrDefault(u => u.Id == id);
        return u is null ? null : UserResponse.FromDomain(u);
    }

    public UserResponse? GetByEmail(string email)
    {
        var u = _ctx.Users.Include(u => u.City).FirstOrDefault(u => u.Email == email);
        return u is null ? null : UserResponse.FromDomain(u);
    }

    public bool ExistsById(Guid id) => _ctx.Users.Any(u => u.Id == id);
    public bool ExistsByEmail(string email) => _ctx.Users.Any(u => u.Email == email);

    public UserResponse Create(UserRequest request)
    {
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var user = new User(
            request.CityId, request.Name, request.Email,
            passwordHash, 
            request.Role, request.Phone, request.IsActive,
            request.NotifyEmail, request.NotifyPush);

        _ctx.Users.Add(user);
        _ctx.SaveChanges();
        var created = _ctx.Users.Include(u => u.City).First(u => u.Id == user.Id);
        return UserResponse.FromDomain(created);
    }

    public UserResponse Update(Guid id, UserRequest request)
    {
        var user = _ctx.Users.Include(u => u.City).First(u => u.Id == id);

        if (!string.IsNullOrWhiteSpace(request.Password))
        {
            var newHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            user.UpdatePassword(newHash);
        }

        user.Update(request.CityId, request.Name, request.Email, request.Role,
            request.Phone, request.IsActive, request.NotifyEmail, request.NotifyPush);
        _ctx.SaveChanges();
        return UserResponse.FromDomain(user);
    }

    public bool Delete(Guid id)
    {
        var user = _ctx.Users.FirstOrDefault(u => u.Id == id);
        if (user is null) return false;
        _ctx.Users.Remove(user);
        _ctx.SaveChanges();
        return true;
    }
}