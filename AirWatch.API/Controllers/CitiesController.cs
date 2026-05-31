using AirWatch.Application.DTOs;
using AirWatch.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AirWatch.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CitiesController : ControllerBase
{
    private readonly ICityRepository _repo;
    private readonly ICountryRepository _countryRepo;
    public CitiesController(ICityRepository repo, ICountryRepository countryRepo)
    { _repo = repo; _countryRepo = countryRepo; }

    [HttpGet]
    public IActionResult GetAll() => Ok(_repo.GetAll());

    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        if (!_repo.ExistsById(id)) return NotFound();
        return Ok(_repo.GetById(id));
    }

    [HttpGet("by-country/{countryId:guid}")]
    public IActionResult GetByCountry(Guid countryId) => Ok(_repo.GetByCountryId(countryId));

    [HttpPost]
    public IActionResult Create([FromBody] CityRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (!_countryRepo.ExistsById(request.CountryId))
            return NotFound(new { message = "País não encontrado." });
        var created = _repo.Create(request);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public IActionResult Update(Guid id, [FromBody] CityRequest request)
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