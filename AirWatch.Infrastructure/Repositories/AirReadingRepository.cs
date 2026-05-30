using AirWatch.Application.DTOs;
using AirWatch.Application.Services;
using AirWatch.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AirWatch.Infrastructure.Repositories;

public class AirReadingRepository :  IAirReadingRepository
{
    private readonly AirWatchContext _ctx;
    public AirReadingRepository(AirWatchContext ctx) => _ctx = ctx;

    public IReadOnlyList<AirReadingResponse> GetAll() =>
        _ctx.AirReadings.AsNoTracking().Include(r => r.City).Include(r => r.Sensor)
            .OrderByDescending(r => r.ReadingAt).Take(100)
            .Select(r => AirReadingResponse.FromDomain(r)).ToList();

    public AirReadingResponse? GetById(Guid id)
    {
        var r = _ctx.AirReadings.Include(r => r.City).Include(r => r.Sensor)
            .FirstOrDefault(r => r.Id == id);
        return r is null ? null : AirReadingResponse.FromDomain(r);
    }

    public IReadOnlyList<AirReadingResponse> GetByCityId(Guid cityId) =>
        _ctx.AirReadings.AsNoTracking().Include(r => r.City).Include(r => r.Sensor)
            .Where(r => r.CityId == cityId).OrderByDescending(r => r.ReadingAt)
            .Select(r => AirReadingResponse.FromDomain(r)).ToList();

    public IReadOnlyList<AirReadingResponse> GetBySensorId(Guid sensorId) =>
        _ctx.AirReadings.AsNoTracking().Include(r => r.City)
            .Where(r => r.SensorId == sensorId).OrderByDescending(r => r.ReadingAt)
            .Select(r => AirReadingResponse.FromDomain(r)).ToList();

    public bool ExistsById(Guid id) => _ctx.AirReadings.Any(r => r.Id == id);

    public AirReadingResponse Create(AirReadingRequest request)
    {
        var reading = request.ToDomain();
        _ctx.AirReadings.Add(reading);
        _ctx.SaveChanges();
        var created = _ctx.AirReadings.Include(r => r.City).Include(r => r.Sensor)
            .First(r => r.Id == reading.Id);
        return AirReadingResponse.FromDomain(created);
    }
}