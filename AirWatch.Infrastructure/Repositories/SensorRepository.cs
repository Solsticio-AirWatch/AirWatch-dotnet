using AirWatch.Application.DTOs;
using AirWatch.Application.Services;
using AirWatch.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AirWatch.Infrastructure.Repositories;

public class SensorRepository : ISensorRepository
{
    private readonly AirWatchContext _ctx;
    public SensorRepository(AirWatchContext ctx) => _ctx = ctx;

    public IReadOnlyList<SensorResponse> GetAll() =>
        _ctx.Sensors.AsNoTracking().Include(s => s.City).OrderBy(s => s.Name)
            .Select(s => SensorResponse.FromDomain(s)).ToList();

    public SensorResponse? GetById(Guid id)
    {
        var s = _ctx.Sensors.Include(s => s.City).FirstOrDefault(s => s.Id == id);
        return s is null ? null : SensorResponse.FromDomain(s);
    }

    public IReadOnlyList<SensorResponse> GetByCityId(Guid cityId) =>
        _ctx.Sensors.AsNoTracking().Include(s => s.City)
            .Where(s => s.CityId == cityId)
            .Select(s => SensorResponse.FromDomain(s)).ToList();

    public bool ExistsById(Guid id) => _ctx.Sensors.Any(s => s.Id == id);

    public SensorResponse Create(SensorRequest request)
    {
        var sensor = request.ToDomain();
        _ctx.Sensors.Add(sensor);
        _ctx.SaveChanges();
        var created = _ctx.Sensors.Include(s => s.City).First(s => s.Id == sensor.Id);
        return SensorResponse.FromDomain(created);
    }

    public SensorResponse Update(Guid id, SensorRequest request)
    {
        var sensor = _ctx.Sensors.Include(s => s.City).First(s => s.Id == id);
        sensor.Update(request.Name, request.Type, request.Location,
            request.Latitude, request.Longitude, request.Source, request.Status);
        _ctx.SaveChanges();
        return SensorResponse.FromDomain(sensor);
    }

    public bool Delete(Guid id)
    {
        var sensor = _ctx.Sensors.FirstOrDefault(s => s.Id == id);
        if (sensor is null) return false;
        _ctx.Sensors.Remove(sensor);
        _ctx.SaveChanges();
        return true;
    }
}