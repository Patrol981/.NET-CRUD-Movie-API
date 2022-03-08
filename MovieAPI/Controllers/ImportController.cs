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
  public class ImportController : ControllerBase {
    private readonly IImportRepoository _importRepo;
    private readonly IDirectorRepository _directorRepo;
    private readonly IMovieRepository _movieRepo;
    private readonly ISerialRepository _serialRepo;
    private readonly ILogger _logger;

    public ImportController
    (IImportRepoository importRepo, ILogger<ImportController> logger,
    IMovieRepository movieRepo, ISerialRepository serialRepo, IDirectorRepository directorRepo) {
      _importRepo = importRepo;
      _logger = logger;
      _movieRepo = movieRepo;
      _directorRepo = directorRepo;
      _serialRepo = serialRepo;
    }

    [HttpPost("ImportDirector")]
    public async Task<IActionResult> PostImportDirector(ImportDirector importDirector) {
      var isExist = _directorRepo.GetDirectorByName(importDirector.Firstname, importDirector.Lastname);
      var isIDExist = await _directorRepo.GetDirector(importDirector.DirectorID);
      if(isExist != null || isIDExist != null) {
        return Conflict();
      }
      await _importRepo.ImportDirector(importDirector);
      return Ok();
    }

    [HttpPost("ImportMovie")]
    public async Task<IActionResult> PostImportMovie(ImportMovie importMovie) {
      var isExist = _movieRepo.GetMovieByTitle(importMovie.MovieTitle);
      var isIDExist = await _movieRepo.GetMovie(importMovie.MovieID);
      if(isExist != null || isIDExist != null) {
        return Conflict();
      }
      await _importRepo.ImportMovie(importMovie);
      return Ok();
    }

    [HttpPost("ImportSerial")]
    public async Task<IActionResult> PostImportSerial(ImportSerial importSerial) {
      var isExist = _serialRepo.GetSerialByTitle(importSerial.SerialTitle);
      var isIDExist = await _serialRepo.GetSerial(importSerial.SerialID);
      if(isExist != null || isIDExist != null) {
        return Conflict();
      }
      await _importRepo.ImportSerial(importSerial);
      return Ok();
    }
  }
}