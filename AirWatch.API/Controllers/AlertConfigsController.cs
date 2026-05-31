using AirWatch.Application.DTOs;
using AirWatch.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AirWatch.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlertConfigsController : ControllerBase
{
    private readonly IAlertConfigRepository _repo;
    private readonly IUserRepository _userRepo;
    private readonly ICityRepository _cityRepo;
    public AlertConfigsController(IAlertConfigRepository repo, IUserRepository userRepo, ICityRepository cityRepo)
    { _repo = repo; _userRepo = userRepo; _cityRepo = cityRepo; }

    [HttpGet]
    public IActionResult GetAll() => Ok(_repo.GetAll());

    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        if (!_repo.ExistsById(id)) return NotFound();
        return Ok(_repo.GetById(id));
    }

    [HttpGet("by-user/{userId:guid}")]
    public IActionResult GetByUser(Guid userId) => Ok(_repo.GetByUserId(userId));

    [HttpGet("by-city/{cityId:guid}")]
    public IActionResult GetByCity(Guid cityId) => Ok(_repo.GetByCityId(cityId));

    [HttpPost]
    public IActionResult Create([FromBody] AlertConfigRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (!_userRepo.ExistsById(request.UserId))
            return NotFound(new { message = "Usuário não encontrado." });
        if (!_cityRepo.ExistsById(request.CityId))
            return NotFound(new { message = "Cidade não encontrada." });
        var created = _repo.Create(request);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public IActionResult Update(Guid id, [FromBody] AlertConfigRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (!_repo.ExistsById(id)) return NotFound();
        return Ok(_repo.Update(id, request));
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        if (!_repo.ExistsById(id)) return NotFound();
        _repo.Delete(id);
        return NoContent();
    }
}