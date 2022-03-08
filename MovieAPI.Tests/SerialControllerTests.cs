using System.Diagnostics;
using System;
using Xunit;
using Xunit.Abstractions;
using Moq;

using MovieAPI.Interfaces;
using MovieAPI.Models;
using MovieAPI.Controllers;

using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieAPI.Tests {
  public class SerialControllerTests {
    private readonly Mock<ISerialRepository> _serialRepo = new();
    private readonly Mock<IDirectorRepository> _directorRepo = new();
    private readonly Mock<ILogger<DirectorController>> _dLogger = new();
    private readonly Mock<ILogger<SerialController>> _sLogger = new();
    private readonly ITestOutputHelper _output;

    public SerialControllerTests(ITestOutputHelper output) {
      _output = output;
    }

    [Fact]
    public async Task GetSerial_WithExistingItem_ShouldReturnItem() {
      var serial = new Serial() {
        DirectorID = new Guid(),
        SerialEpisodes = 12,
        SerialTitle = "A Serial",
        ProductionYear = System.DateTime.Now
      };
      _serialRepo.Setup(repo => repo.GetSerial(It.IsAny<Guid>())).ReturnsAsync((Serial)serial);

      var controller = new SerialController(_serialRepo.Object, _sLogger.Object);
      var result = await controller.GetSerial(Guid.NewGuid());

      Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetSerial_WithUnExistingItem_ShouldReturnNotFound() {
      _serialRepo.Setup(repo => repo.GetSerial(It.IsAny<Guid>())).ReturnsAsync((Serial)null);

      var controller = new SerialController(_serialRepo.Object, _sLogger.Object);
      var result = await controller.GetSerial(Guid.NewGuid());

      Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void GetSerials_ShouldReturnList() {
      _serialRepo.Setup(repo => repo.GetSerials()).Returns(new List<Serial>());

      var controller = new SerialController(_serialRepo.Object, _sLogger.Object);
      var result = controller.GetSerials();

      var list = (result as OkObjectResult).Value as List<Serial>;

      Assert.IsType<OkObjectResult>(result);
      Assert.IsType<List<Serial>>(list);
    }

    [Fact]
    public async Task PostSerial_WithEmptyDB_ShouldReturnCreatedOk() {
      var director = new Director() {
        Firstname = "Kacper",
        Lastname = "Adler"
      };

      _directorRepo.Setup(repo => repo.GetDirector(It.IsAny<Guid>())).ReturnsAsync((Director)director);

      var serial = new Serial() {
        DirectorID = new Guid(),
        SerialEpisodes = 12,
        SerialTitle = "A Serial",
        ProductionYear = System.DateTime.Now
      };

      var controller = new SerialController(_serialRepo.Object, _sLogger.Object);
      var result = await controller.PostSerial(serial);
      var createdSerial = (result as CreatedAtActionResult).Value as Serial;

      Assert.NotNull(createdSerial);
      Assert.Equal(director.DirectorID, createdSerial.DirectorID);
      Assert.Equal(12, createdSerial.SerialEpisodes);
      Assert.Equal("A Serial", createdSerial.SerialTitle);
    }

    [Fact]
    public async Task PostSerial_WithNonEmptyDB_ShouldReturnConflict() {
      var director = new Director() {
        Firstname = "Kacper",
        Lastname = "Adler"
      };
      var serial = new Serial() {
        DirectorID = new Guid(),
        SerialEpisodes = 12,
        SerialTitle = "A Serial",
        ProductionYear = System.DateTime.Now
      };

      _directorRepo.Setup(repo => repo.GetDirector(It.IsAny<Guid>())).ReturnsAsync((Director)director);
      _serialRepo.Setup(repo => repo.GetSerialByTitle(It.IsAny<string>())).Returns((Serial)serial);

      var controller = new SerialController(_serialRepo.Object, _sLogger.Object);
      var result = await controller.PostSerial(serial);

      Assert.IsType<ConflictResult>(result);
    }

    [Fact]
    public async Task DeleteSerial_ShouldReturnOk() {
      var serial = new Serial() {
        DirectorID = new Guid(),
        SerialEpisodes = 12,
        SerialTitle = "A Serial",
        ProductionYear = System.DateTime.Now
      };

      _serialRepo.Setup(repo => repo.GetSerialByTitle(It.IsAny<string>())).Returns((Serial)serial);
      _serialRepo.Setup(repo => repo.GetSerial(It.IsAny<Guid>())).ReturnsAsync((Serial)serial);
      var controller = new SerialController(_serialRepo.Object, _sLogger.Object);
      var result = await controller.DeleteSerial(serial.SerialID);

      Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task UpdateSerial_ShouldReturnOk() {
      var serial = new Serial() {
        DirectorID = new Guid(),
        SerialEpisodes = 12,
        SerialTitle = "A Serial",
        ProductionYear = System.DateTime.Now
      };

      var serial2 = new Serial() {
        DirectorID = new Guid(),
        SerialEpisodes = 14,
        SerialTitle = "A Serial 2",
        ProductionYear = System.DateTime.Now
      };

      _serialRepo.Setup(repo => repo.GetSerialByTitle(It.IsAny<string>())).Returns((Serial)serial);
      _serialRepo.Setup(repo => repo.GetSerial(It.IsAny<Guid>())).ReturnsAsync((Serial)serial);
      var controller = new SerialController(_serialRepo.Object, _sLogger.Object);
      var result = await controller.UpdateSerial(new Guid(), serial2);

      Assert.IsType<OkObjectResult>(result);
    }
  }
}