using AirWatch.Application.DTOs;
using AirWatch.Application.Services;
using AirWatch.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AirWatch.Infrastructure.Repositories;

public class CountryRepository : ICountryRepository
{
    private readonly AirWatchContext _ctx;
    public CountryRepository(AirWatchContext ctx) => _ctx = ctx;

    public IReadOnlyList<CountryResponse> GetAll() =>
        _ctx.Countries.AsNoTracking().OrderBy(c => c.Name)
            .Select(c => CountryResponse.FromDomain(c)).ToList();

    public CountryResponse? GetById(Guid id)
    {
        var c = _ctx.Countries.FirstOrDefault(c => c.Id == id);
        return c is null ? null : CountryResponse.FromDomain(c);
    }

    public CountryResponse? GetByIsoCode(string isoCode)
    {
        var c = _ctx.Countries.FirstOrDefault(c => c.IsoCode == isoCode.ToUpper());
        return c is null ? null : CountryResponse.FromDomain(c);
    }

    public bool ExistsById(Guid id) => _ctx.Countries.Any(c => c.Id == id);

    public CountryResponse Create(CountryRequest request)
    {
        var country = request.ToDomain();
        _ctx.Countries.Add(country);
        _ctx.SaveChanges();
        return CountryResponse.FromDomain(country);
    }

    public CountryResponse Update(Guid id, CountryRequest request)
    {
        var country = _ctx.Countries.First(c => c.Id == id);
        country.Update(request.Name, request.IsoCode, request.Continent);
        _ctx.SaveChanges();
        return CountryResponse.FromDomain(country);
    }

    public bool Delete(Guid id)
    {
        var country = _ctx.Countries.FirstOrDefault(c => c.Id == id);
        if (country is null) return false;
        _ctx.Countries.Remove(country);
        _ctx.SaveChanges();
        return true;
    }
}