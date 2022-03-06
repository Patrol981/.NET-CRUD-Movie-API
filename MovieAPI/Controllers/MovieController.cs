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
  public class MovieController : ControllerBase {
    private readonly IMovieRepository _movieRepo;
    private readonly IDirectorRepository _directorRepo;
    private readonly ILogger _logger;
    public MovieController(IMovieRepository movieRepo, IDirectorRepository directorRepo, ILogger<MovieController> logger) {
      _movieRepo = movieRepo;
      _directorRepo = directorRepo;
      _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> PostMovie(Movie movie) {
      if(MovieValidator.CheckMovie(movie) == EValidator.InValid) return BadRequest(movie);
      if(await _directorRepo.GetDirector(movie.Director) == null) return NotFound(movie.Director);
      var isExist = _movieRepo.GetMovieByTitle(movie.MovieTitle);
      if(isExist != null) {
        return Conflict();
      }
      await _movieRepo.AddMovie(movie);
      return CreatedAtAction(nameof(GetMovie), new {id = movie.MovieID}, movie);
    }

    [HttpGet]
    public IActionResult GetMovies() {
      return Ok(_movieRepo.GetMovies());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Movie>> GetMovie(Guid id) {
      var isExist = await _movieRepo.GetMovie(id);
      if(isExist == null) {
        return NotFound();
      }
      return Ok(isExist);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMovie(Guid id) {
      var isExist = await _movieRepo.GetMovie(id);
      if(isExist == null) {
        return NotFound();
      }
      _movieRepo.DeleteMovie(isExist);
      return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMovie(Guid id, Movie movie) {
      if(MovieValidator.CheckMovie(movie) == EValidator.InValid) return BadRequest(movie);
      var isExist = await _movieRepo.GetMovie(id);
      if(isExist == null) {
        return NotFound();
      }
      return new OkObjectResult(await _movieRepo.UpdateMovie(id, movie));
    }
  }
}