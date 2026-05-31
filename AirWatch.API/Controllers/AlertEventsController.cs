using AirWatch.Application.DTOs;
using AirWatch.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AirWatch.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlertEventsController : ControllerBase
{
    private readonly IAlertEventRepository _repo;
    private readonly IAlertConfigRepository _configRepo;
    private readonly IAirReadingRepository _readingRepo;
    public AlertEventsController(IAlertEventRepository repo, IAlertConfigRepository configRepo, IAirReadingRepository readingRepo)
    { _repo = repo; _configRepo = configRepo; _readingRepo = readingRepo; }

    [HttpGet]
    public IActionResult GetAll() => Ok(_repo.GetAll());

    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        if (!_repo.ExistsById(id)) return NotFound();
        return Ok(_repo.GetById(id));
    }

    [HttpGet("by-config/{alertConfigId:guid}")]
    public IActionResult GetByAlertConfig(Guid alertConfigId) => Ok(_repo.GetByAlertConfigId(alertConfigId));

    [HttpPost]
    public IActionResult Create([FromBody] AlertEventRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (!_configRepo.ExistsById(request.AlertConfigId))
            return NotFound(new { message = "AlertConfig não encontrado." });
        if (!_readingRepo.ExistsById(request.ReadingId))
            return NotFound(new { message = "AirReading não encontrado." });
        var created = _repo.Create(request);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPatch("{id:guid}/notify")]
    public IActionResult MarkNotified(Guid id)
    {
        if (!_repo.ExistsById(id)) return NotFound();
        return Ok(_repo.MarkNotified(id));
    }
}