using AirWatch.Application.DTOs;

namespace AirWatch.Application.Services;

public interface ICityRepository
{
    IReadOnlyList<CityResponse> GetAll();
    CityResponse? GetById(Guid id);
    IReadOnlyList<CityResponse> GetByCountryId(Guid countryId);
    bool ExistsById(Guid id);
    CityResponse Create(CityRequest request);
    CityResponse Update(Guid id, CityRequest request);
    bool Delete(Guid id);
}