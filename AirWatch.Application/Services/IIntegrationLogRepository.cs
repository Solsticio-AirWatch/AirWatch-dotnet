using AirWatch.Application.DTOs;

namespace AirWatch.Application.Services;

public interface IIntegrationLogRepository
{
    IReadOnlyList<IntegrationLogResponse> GetAll();
    IntegrationLogResponse? GetById(Guid id);
    IReadOnlyList<IntegrationLogResponse> GetByCityId(Guid cityId);
    bool ExistsById(Guid id);
    IntegrationLogResponse Create(IntegrationLogRequest request);

}