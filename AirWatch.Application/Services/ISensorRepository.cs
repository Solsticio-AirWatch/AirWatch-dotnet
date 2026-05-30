using AirWatch.Application.DTOs;

namespace AirWatch.Application.Services;

public interface ISensorRepository
{
    IReadOnlyList<SensorResponse> GetAll();
    SensorResponse? GetById(Guid id);
    IReadOnlyList<SensorResponse> GetByCityId(Guid cityId);
    bool ExistsById(Guid id);
    SensorResponse Create(SensorRequest request);
    SensorResponse Update(Guid id, SensorRequest request);
    bool Delete(Guid id);
}