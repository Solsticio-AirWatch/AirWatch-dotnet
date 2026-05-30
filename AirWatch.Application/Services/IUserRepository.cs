using AirWatch.Application.DTOs;

namespace AirWatch.Application.Services;

public interface IUserRepository
{
    IReadOnlyList<UserResponse> GetAll();
    UserResponse? GetById(Guid id);
    UserResponse? GetByEmail(string email);
    bool ExistsById(Guid id);
    bool ExistsByEmail(string email);
    UserResponse Create(UserRequest request);
    UserResponse Update(Guid id, UserRequest request);
    bool Delete(Guid id);
}