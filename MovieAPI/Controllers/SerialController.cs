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
  public class SerialController : ControllerBase {
    private readonly ISerialRepository _serialRepo;
    private readonly IDirectorRepository _directorRepo;
    private readonly ILogger _logger;

    public SerialController(ISerialRepository serialRepo, ILogger<SerialController> logger, IDirectorRepository directorRepo) {
      _serialRepo = serialRepo;
      _logger = logger;
      _directorRepo = directorRepo;
    }

    [HttpPost]
    public async Task<IActionResult> PostSerial(Serial serial) {
      serial.SerialID = Guid.NewGuid();
      if(SerialValidator.CheckSerial(serial) == EValidator.InValid) return BadRequest(serial);
      if(await _directorRepo.GetDirector(serial.DirectorID) == null) return NotFound(serial.DirectorID);
      if(await _serialRepo.GetSerial(serial.SerialID) != null) return Conflict();
      var isExist =  _serialRepo.GetSerialByTitle(serial.SerialTitle);
      if(isExist != null) {
        return Conflict();
      }
      await _serialRepo.AddSerial(serial);
      return CreatedAtAction(nameof(GetSerial), new {id = serial.SerialID}, serial);
    }

    [HttpGet]
    public IActionResult GetSerials() {
      return Ok(_serialRepo.GetSerials());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSerial(Guid id) {
      var isExist = await _serialRepo.GetSerial(id);
      if(isExist == null) {
        return NotFound();
      }
      return Ok(isExist);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSerial(Guid id) {
      var isExist = await _serialRepo.GetSerial(id);
      if(isExist == null) {
        return NotFound();
      }
      _serialRepo.DeleteSerial(isExist);
      return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSerial(Guid id, Serial serial) {
      if(SerialValidator.CheckSerial(serial) == EValidator.InValid) return BadRequest(serial);
      var isExist = await _serialRepo.GetSerial(id);
      if(isExist == null) {
        return NotFound();
      }
      return new OkObjectResult(await _serialRepo.UpdateSerial(id, serial));
    }
  }
}