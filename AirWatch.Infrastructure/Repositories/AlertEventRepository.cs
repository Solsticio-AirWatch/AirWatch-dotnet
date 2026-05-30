using AirWatch.Application.DTOs;
using AirWatch.Application.Services;
using AirWatch.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AirWatch.Infrastructure.Repositories;

public class AlertEventRepository : IAlertEventRepository
{
    private readonly AirWatchContext _ctx;
    public AlertEventRepository(AirWatchContext ctx) => _ctx = ctx;

    public IReadOnlyList<AlertEventResponse> GetAll() =>
        _ctx.AlertEvents.AsNoTracking().Include(e => e.AlertConfig).Include(e => e.AirReading)
            .OrderByDescending(e => e.EventAt).Take(100)
            .Select(e => AlertEventResponse.FromDomain(e)).ToList();

    public AlertEventResponse? GetById(Guid id)
    {
        var e = _ctx.AlertEvents.Include(e => e.AlertConfig).Include(e => e.AirReading)
            .FirstOrDefault(e => e.Id == id);
        return e is null ? null : AlertEventResponse.FromDomain(e);
    }

    public IReadOnlyList<AlertEventResponse> GetByAlertConfigId(Guid alertConfigId) =>
        _ctx.AlertEvents.AsNoTracking().Include(e => e.AlertConfig).Include(e => e.AirReading)
            .Where(e => e.AlertConfigId == alertConfigId).OrderByDescending(e => e.EventAt)
            .Select(e => AlertEventResponse.FromDomain(e)).ToList();

    public bool ExistsById(Guid id) => _ctx.AlertEvents.Any(e => e.Id == id);

    public AlertEventResponse Create(AlertEventRequest request)
    {
        var ev = request.ToDomain();
        _ctx.AlertEvents.Add(ev);
        _ctx.SaveChanges();
        var created = _ctx.AlertEvents.Include(e => e.AlertConfig).Include(e => e.AirReading)
            .First(e => e.Id == ev.Id);
        return AlertEventResponse.FromDomain(created);
    }

    public AlertEventResponse MarkNotified(Guid id)
    {
        var ev = _ctx.AlertEvents.Include(e => e.AlertConfig).Include(e => e.AirReading)
            .First(e => e.Id == id);
        ev.MarkNotified();
        _ctx.SaveChanges();
        return AlertEventResponse.FromDomain(ev);
    }
}