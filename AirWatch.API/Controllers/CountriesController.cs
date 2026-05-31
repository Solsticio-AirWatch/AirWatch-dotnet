using AirWatch.Application.DTOs;
using AirWatch.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AirWatch.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CountriesController : ControllerBase
{
    private readonly ICountryRepository _repo;
    public CountriesController(ICountryRepository repo) => _repo = repo;

    [HttpGet]
    public IActionResult GetAll() => Ok(_repo.GetAll());

    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        if (!_repo.ExistsById(id)) return NotFound();
        return Ok(_repo.GetById(id));
    }

    [HttpPost]
    public IActionResult Create([FromBody] CountryRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (_repo.GetByIsoCode(request.IsoCode) is not null)
            return Conflict(new { message = $"País com ISO '{request.IsoCode}' já existe." });

        try
        {
            var created = _repo.Create(request);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (DbUpdateException)
        {
            return Conflict(new { message = $"País com ISO '{request.IsoCode}' já existe." });
        }
    }

    [HttpPut("{id:guid}")]
    public IActionResult Update(Guid id, [FromBody] CountryRequest request)
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