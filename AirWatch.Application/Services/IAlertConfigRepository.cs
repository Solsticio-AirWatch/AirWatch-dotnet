using AirWatch.Application.DTOs;

namespace AirWatch.Application.Services;

public interface IAlertConfigRepository
{
    IReadOnlyList<AlertConfigResponse> GetAll();
    AlertConfigResponse? GetById(Guid id);
    IReadOnlyList<AlertConfigResponse> GetByUserId(Guid userId);
    IReadOnlyList<AlertConfigResponse> GetByCityId(Guid cityId);
    bool ExistsById(Guid id);
    AlertConfigResponse Create(AlertConfigRequest request);
    AlertConfigResponse Update(Guid id, AlertConfigRequest request);
    bool Delete(Guid id);
}