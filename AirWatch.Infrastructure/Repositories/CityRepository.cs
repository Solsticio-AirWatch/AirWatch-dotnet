using AirWatch.Application.DTOs;
using AirWatch.Application.Services;
using AirWatch.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AirWatch.Infrastructure.Repositories;

public class CityRepository : ICityRepository
{
    private readonly AirWatchContext _ctx;
    public CityRepository(AirWatchContext ctx) => _ctx = ctx;

    public IReadOnlyList<CityResponse> GetAll() =>
        _ctx.Cities.AsNoTracking().Include(c => c.Country).OrderBy(c => c.Name)
            .Select(c => CityResponse.FromDomain(c)).ToList();

    public CityResponse? GetById(Guid id)
    {
        var c = _ctx.Cities.Include(c => c.Country).FirstOrDefault(c => c.Id == id);
        return c is null ? null : CityResponse.FromDomain(c);
    }

    public IReadOnlyList<CityResponse> GetByCountryId(Guid countryId) =>
        _ctx.Cities.AsNoTracking().Include(c => c.Country)
            .Where(c => c.CountryId == countryId).OrderBy(c => c.Name)
            .Select(c => CityResponse.FromDomain(c)).ToList();

    public bool ExistsById(Guid id) => _ctx.Cities.Any(c => c.Id == id);

    public CityResponse Create(CityRequest request)
    {
        var city = request.ToDomain();
        _ctx.Cities.Add(city);
        _ctx.SaveChanges();
        var created = _ctx.Cities.Include(c => c.Country).First(c => c.Id == city.Id);
        return CityResponse.FromDomain(created);
    }

    public CityResponse Update(Guid id, CityRequest request)
    {
        var city = _ctx.Cities.Include(c => c.Country).First(c => c.Id == id);
        city.Update(request.Name, request.State, request.Latitude, request.Longitude,
            request.AltitudeM, request.Population, request.Status);
        _ctx.SaveChanges();
        return CityResponse.FromDomain(city);
    }

    public bool Delete(Guid id)
    {
        var city = _ctx.Cities.FirstOrDefault(c => c.Id == id);
        if (city is null) return false;
        _ctx.Cities.Remove(city);
        _ctx.SaveChanges();
        return true;
    }
}