using AirWatch.Application.DTOs;
using AirWatch.Application.Services;
using AirWatch.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AirWatch.Infrastructure.Repositories;

public class AlertConfigRepository : IAlertConfigRepository
{
    private readonly AirWatchContext _ctx;
    public AlertConfigRepository(AirWatchContext ctx) => _ctx = ctx;

    public IReadOnlyList<AlertConfigResponse> GetAll() =>
        _ctx.AlertConfigs.AsNoTracking().Include(a => a.User).Include(a => a.City)
            .OrderBy(a => a.Severity)
            .Select(a => AlertConfigResponse.FromDomain(a)).ToList();

    public AlertConfigResponse? GetById(Guid id)
    {
        var a = _ctx.AlertConfigs.Include(a => a.User).Include(a => a.City)
            .FirstOrDefault(a => a.Id == id);
        return a is null ? null : AlertConfigResponse.FromDomain(a);
    }

    public IReadOnlyList<AlertConfigResponse> GetByUserId(Guid userId) =>
        _ctx.AlertConfigs.AsNoTracking().Include(a => a.User).Include(a => a.City)
            .Where(a => a.UserId == userId)
            .Select(a => AlertConfigResponse.FromDomain(a)).ToList();

    public IReadOnlyList<AlertConfigResponse> GetByCityId(Guid cityId) =>
        _ctx.AlertConfigs.AsNoTracking().Include(a => a.User).Include(a => a.City)
            .Where(a => a.CityId == cityId)
            .Select(a => AlertConfigResponse.FromDomain(a)).ToList();

    public bool ExistsById(Guid id) => _ctx.AlertConfigs.Any(a => a.Id == id);

    public AlertConfigResponse Create(AlertConfigRequest request)
    {
        var config = request.ToDomain();
        _ctx.AlertConfigs.Add(config);
        _ctx.SaveChanges();
        var created = _ctx.AlertConfigs.Include(a => a.User).Include(a => a.City)
            .First(a => a.Id == config.Id);
        return AlertConfigResponse.FromDomain(created);
    }

    public AlertConfigResponse Update(Guid id, AlertConfigRequest request)
    {
        var config = _ctx.AlertConfigs.Include(a => a.User).Include(a => a.City)
            .First(a => a.Id == id);
        config.Update(request.Pollutant, request.Threshold, request.Operator,
                      request.Severity, request.IsActive);
        _ctx.SaveChanges();
        return AlertConfigResponse.FromDomain(config);
    }

    public bool Delete(Guid id)
    {
        var config = _ctx.AlertConfigs.FirstOrDefault(a => a.Id == id);
        if (config is null) return false;
        _ctx.AlertConfigs.Remove(config);
        _ctx.SaveChanges();
        return true;
    }
}