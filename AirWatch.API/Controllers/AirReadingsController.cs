using AirWatch.Application.DTOs;
using AirWatch.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AirWatch.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AirReadingsController : ControllerBase
{
    private readonly IAirReadingRepository _repo;
    private readonly ICityRepository _cityRepo;
    private readonly ISensorRepository _sensorRepo;
    public AirReadingsController(IAirReadingRepository repo, ICityRepository cityRepo, ISensorRepository sensorRepo)
    { _repo = repo; _cityRepo = cityRepo; _sensorRepo = sensorRepo; }

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

    [HttpGet("by-sensor/{sensorId:guid}")]
    public IActionResult GetBySensor(Guid sensorId) => Ok(_repo.GetBySensorId(sensorId));

    [HttpPost]
    public IActionResult Create([FromBody] AirReadingRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (!_cityRepo.ExistsById(request.CityId))
            return NotFound(new { message = "Cidade não encontrada." });
        if (request.SensorId.HasValue && !_sensorRepo.ExistsById(request.SensorId.Value))
            return NotFound(new { message = "Sensor não encontrado." });
        var created = _repo.Create(request);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }
}