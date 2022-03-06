using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieAPI.Models;
using MovieAPI.Interfaces;
using MovieAPI.Validators;
using MovieAPI.Enums;

namespace MovieAPI.Controllers {
  [Route("api/[controller]")]
  [ApiController]
  public class DirectorController : ControllerBase {
    private readonly IDirectorRepository _directorRepo;
    private readonly ILogger _logger;

    public DirectorController(IDirectorRepository directorRepo, ILogger<DirectorController> logger) {
      _directorRepo = directorRepo;
      _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> PostDirector(Director director) {
      if(DirectorValidator.CheckDirector(director) == EValidator.InValid) {
        return BadRequest(director);
      }
      var isExist = _directorRepo.GetDirectorByName(director.Firstname, director.Lastname);
      if(isExist != null) {
        return Conflict();
      }
      await _directorRepo.AddDirector(director);
      return CreatedAtAction(nameof(GetDirector), new {id = director.DirectorID}, director);
    }

    [HttpGet]
    public IActionResult GetDirectors() {
      return Ok(_directorRepo.GetDirectors());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Director>> GetDirector(Guid id) {
      var isExist = await _directorRepo.GetDirector(id);
      if(isExist == null) {
        return NotFound();
      }
      return Ok(isExist);
    }

    [HttpGet("{id}/GetDirectorWorks/")]
    public async Task<ActionResult<MovieSerial>> GetDirectorWorks(Guid id) {
      return Ok(await _directorRepo.GetDirectorWorks(id));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDirector(Guid id) {
      var isExist = await _directorRepo.GetDirector(id);
      if(isExist == null) {
        return NotFound();
      }
      _directorRepo.DeleteDirector(isExist);
      return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDirector(Guid id, Director director) {
      if(DirectorValidator.CheckDirector(director) == EValidator.InValid) return BadRequest(director);
      var isExist = await _directorRepo.GetDirector(id);
      if(isExist == null) {
        return NotFound();
      }
      return new OkObjectResult(await _directorRepo.UpdateDirector(id, director));
    }
  }
}