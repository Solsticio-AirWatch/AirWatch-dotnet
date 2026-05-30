using AirWatch.Application.DTOs;

namespace AirWatch.Application.Services;

public interface IAirReadingRepository
{
    IReadOnlyList<AirReadingResponse> GetAll();
    AirReadingResponse? GetById(Guid id);
    IReadOnlyList<AirReadingResponse> GetByCityId(Guid cityId);
    IReadOnlyList<AirReadingResponse> GetBySensorId(Guid sensorId);
    bool ExistsById(Guid id);
    AirReadingResponse Create(AirReadingRequest request);
}