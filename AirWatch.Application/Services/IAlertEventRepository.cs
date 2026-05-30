using AirWatch.Application.DTOs;

namespace AirWatch.Application.Services;

public interface IAlertEventRepository
{
    IReadOnlyList<AlertEventResponse> GetAll();
    AlertEventResponse? GetById(Guid id);
    IReadOnlyList<AlertEventResponse> GetByAlertConfigId(Guid alertConfigId);
    bool ExistsById(Guid id);
    AlertEventResponse Create(AlertEventRequest request);
    AlertEventResponse MarkNotified(Guid id);
}