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
  public class DirectorControllerTests {
    private readonly Mock<IDirectorRepository> _directorRepo = new();
    private readonly Mock<ILogger<DirectorController>> _dLogger = new();
    private readonly ITestOutputHelper _output;

    public DirectorControllerTests(ITestOutputHelper output) {
      _output = output;
    }

    [Fact]
    public async Task GetDirector_WithExistingItem_ReturnsDirector() {
      var director = new Director() {
        Firstname = "Dawid",
        Lastname = "Matuszczak"
      };

      _directorRepo.Setup(repo => repo.GetDirector(It.IsAny<Guid>())).ReturnsAsync((Director)director);
      var controller = new DirectorController(_directorRepo.Object, _dLogger.Object);
      var result = await controller.GetDirector(director.DirectorID);
      var gDirector = (result as OkObjectResult).Value as Director;

      Assert.IsType<OkObjectResult>(result);
      Assert.Equal(director.Firstname, gDirector.Firstname);
      Assert.Equal(director.Lastname, gDirector.Lastname);
      Assert.Equal(director.DirectorID, gDirector.DirectorID);
    }

    [Fact]
    public void GetDirectorByName_WithExistingItem_ReturnsDirector() {
      var director = new Director() {
        Firstname = "Dawid",
        Lastname = "Matuszczak"
      };

      _directorRepo.Setup(repo => repo.GetDirectorByName(It.IsAny<string>(), It.IsAny<string>()))
        .Returns((Director)director);
      var controller = new DirectorController(_directorRepo.Object, _dLogger.Object);
      var result = controller.GetDirectorByName(director.Firstname, director.Lastname);
      var gDirector = (result as OkObjectResult).Value as Director;

      Assert.IsType<OkObjectResult>(result);
      Assert.Equal(director.Firstname, gDirector.Firstname);
      Assert.Equal(director.Lastname, gDirector.Lastname);
      Assert.Equal(director.DirectorID, gDirector.DirectorID);
    }

    [Fact]
    public async Task PostDirector_WithEmptyDB_ShouldReturnOk() {
      var director = new Director() {
        Firstname = "Dawid",
        Lastname = "Matuszczak"
      };

      _directorRepo.Setup(repo => repo.GetDirectorByName(It.IsAny<string>(), It.IsAny<string>()))
        .Returns((Director)null);
      _directorRepo.Setup(repo => repo.GetDirector(It.IsAny<Guid>()))
        .ReturnsAsync((Director)null);

      var controller = new DirectorController(_directorRepo.Object, _dLogger.Object);
      var result = await controller.PostDirector(director);

      Assert.IsType<CreatedAtActionResult>(result);
    }

    [Fact]
    public async Task PostDirector_WithNonEmptyDB_ShouldReturnConflict() {
      var director = new Director() {
        Firstname = "Dawid",
        Lastname = "Matuszczak"
      };

      _directorRepo.Setup(repo => repo.GetDirectorByName(It.IsAny<string>(), It.IsAny<string>()))
        .Returns((Director)director);
      _directorRepo.Setup(repo => repo.GetDirector(It.IsAny<Guid>()))
        .ReturnsAsync((Director)director);

      var controller = new DirectorController(_directorRepo.Object, _dLogger.Object);
      var result = await controller.PostDirector(director);

      Assert.IsType<ConflictResult>(result);
    }

    [Fact]
    public async Task DeleteDirector_ShouldReturnOk() {
      var director = new Director() {
        Firstname = "Dawid",
        Lastname = "Matuszczak"
      };

      _directorRepo.Setup(repo => repo.GetDirectorByName(It.IsAny<string>(), It.IsAny<string>()))
        .Returns((Director)director);
      _directorRepo.Setup(repo => repo.GetDirector(It.IsAny<Guid>()))
        .ReturnsAsync((Director)director);

      var controller = new DirectorController(_directorRepo.Object, _dLogger.Object);
      var result = await controller.DeleteDirector(director.DirectorID);

      Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task UpdateDirector_ShouldReturnOk() {
      var director = new Director() {
        Firstname = "Dawid",
        Lastname = "Matuszczak"
      };

      var director2 = new Director() {
        Firstname = "Dawid",
        Lastname = "Domin"
      };

      _directorRepo.Setup(repo => repo.GetDirectorByName(It.IsAny<string>(), It.IsAny<string>()))
        .Returns((Director)director);
      _directorRepo.Setup(repo => repo.GetDirector(It.IsAny<Guid>()))
        .ReturnsAsync((Director)director);

      var controller = new DirectorController(_directorRepo.Object, _dLogger.Object);
      var result = await controller.UpdateDirector(director.DirectorID, director2);

      Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void GetDirectorMovies_ShouldRetrunList() {
      List<MovieDirector> movieDirectors = new List<MovieDirector>();
      _directorRepo.Setup(repo => repo.GetDirectorMovies(It.IsAny<Guid>()))
        .Returns((List<MovieDirector>)movieDirectors);

      var controller = new DirectorController(_directorRepo.Object, _dLogger.Object);
      var result = controller.GetDirectorMovies(Guid.NewGuid());

      var list = (result as OkObjectResult).Value as List<MovieDirector>;


      Assert.IsType<List<MovieDirector>>(list);
    }

    [Fact]
    public void GetDirectorSerials_ShouldRetrunList() {
      List<SerialDirector> serialsDirectors = new List<SerialDirector>();
      _directorRepo.Setup(repo => repo.GetDirectorSerials(It.IsAny<Guid>()))
        .Returns((List<SerialDirector>)serialsDirectors);

      var controller = new DirectorController(_directorRepo.Object, _dLogger.Object);
      var result = controller.GetDirectorSerials(Guid.NewGuid());

      var list = (result as OkObjectResult).Value as List<SerialDirector>;


      Assert.IsType<List<SerialDirector>>(list);
    }
  }
}