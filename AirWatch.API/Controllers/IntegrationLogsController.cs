using AirWatch.Application.DTOs;
using AirWatch.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AirWatch.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IntegrationLogsController : ControllerBase
{
    private readonly IIntegrationLogRepository _repo;
    private readonly ICityRepository _cityRepo;
    public IntegrationLogsController(IIntegrationLogRepository repo, ICityRepository cityRepo)
    { _repo = repo; _cityRepo = cityRepo; }

    [HttpGet]
    public IActionResult GetAll() => Ok(_repo.GetAll());

    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        if (!_repo.ExistsById(id)) return NotFound();
        return Ok(_repo.GetById(id));
    }

    [HttpGet("by-city/{cityId:guid}")]
    public IActionResult GetByCity(Guid cityId) => Ok(_repo.GetByCityId(cityId));

    [HttpPost]
    public IActionResult Create([FromBody] IntegrationLogRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (request.CityId.HasValue && !_cityRepo.ExistsById(request.CityId.Value))
            return NotFound(new { message = "Cidade não encontrada." });
        var created = _repo.Create(request);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }
}