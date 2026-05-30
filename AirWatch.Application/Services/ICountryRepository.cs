using AirWatch.Application.DTOs;

namespace AirWatch.Application.Services;

public interface ICountryRepository
{
    IReadOnlyList<CountryResponse> GetAll();
    CountryResponse? GetById(Guid id);
    CountryResponse? GetByIsoCode(string isoCode);
    bool ExistsById(Guid id);
    CountryResponse Create(CountryRequest request);
    CountryResponse Update(Guid id, CountryRequest request);
    bool Delete(Guid id);
}