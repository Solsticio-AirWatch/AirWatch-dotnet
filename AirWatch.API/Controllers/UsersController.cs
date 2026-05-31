using AirWatch.Application.DTOs;
using AirWatch.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AirWatch.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _repo;
    public UsersController(IUserRepository repo) => _repo = repo;

    [HttpGet]
    public IActionResult GetAll() => Ok(_repo.GetAll());

    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        if (!_repo.ExistsById(id)) return NotFound();
        return Ok(_repo.GetById(id));
    }

    [HttpPost]
    public IActionResult Create([FromBody] UserRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (_repo.ExistsByEmail(request.Email))
            return Conflict(new { message = "E-mail já cadastrado." });
        var created = _repo.Create(request);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public IActionResult Update(Guid id, [FromBody] UserRequest request)
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