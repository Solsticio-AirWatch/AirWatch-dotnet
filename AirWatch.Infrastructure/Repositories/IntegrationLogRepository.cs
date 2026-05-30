using AirWatch.Application.DTOs;
using AirWatch.Application.Services;
using AirWatch.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AirWatch.Infrastructure.Repositories;

public class IntegrationLogRepository : IIntegrationLogRepository
{
    private readonly AirWatchContext _ctx;
    public IntegrationLogRepository(AirWatchContext ctx) => _ctx = ctx;

    public IReadOnlyList<IntegrationLogResponse> GetAll() =>
        _ctx.IntegrationLogs.AsNoTracking().Include(l => l.City)
            .OrderByDescending(l => l.RequestedAt).Take(100)
            .Select(l => IntegrationLogResponse.FromDomain(l)).ToList();

    public IntegrationLogResponse? GetById(Guid id)
    {
        var l = _ctx.IntegrationLogs.Include(l => l.City).FirstOrDefault(l => l.Id == id);
        return l is null ? null : IntegrationLogResponse.FromDomain(l);
    }

    public IReadOnlyList<IntegrationLogResponse> GetByCityId(Guid cityId) =>
        _ctx.IntegrationLogs.AsNoTracking().Include(l => l.City)
            .Where(l => l.CityId == cityId).OrderByDescending(l => l.RequestedAt)
            .Select(l => IntegrationLogResponse.FromDomain(l)).ToList();

    public bool ExistsById(Guid id) => _ctx.IntegrationLogs.Any(l => l.Id == id);

    public IntegrationLogResponse Create(IntegrationLogRequest request)
    {
        var log = request.ToDomain();
        _ctx.IntegrationLogs.Add(log);
        _ctx.SaveChanges();
        var created = _ctx.IntegrationLogs.Include(l => l.City).First(l => l.Id == log.Id);
        return IntegrationLogResponse.FromDomain(created);
    }
}