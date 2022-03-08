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
	public class MovieControllerTests {
    private readonly Mock<IMovieRepository> _movieRepo = new();
    private readonly Mock<IDirectorRepository> _directorRepo = new();
    private readonly Mock<ILogger<MovieController>> _mLogger = new();
    private readonly Mock<ILogger<DirectorController>> _dLogger = new();
    private readonly ITestOutputHelper _output;

    public MovieControllerTests(ITestOutputHelper output) {
      _output = output;
    }
    [Fact]
    public async Task GetMovie_WithUnexistingItem_ReturnsNotFound() {
      _movieRepo.Setup(repo => repo.GetMovie(It.IsAny<Guid>()))
                .ReturnsAsync((Movie)null);

      var controller = new MovieController(_movieRepo.Object, _directorRepo.Object, _mLogger.Object);

      var result = await controller.GetMovie(Guid.NewGuid());

      Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void GetMovies_ShouldReturnOk() {
      _movieRepo.Setup(repo => repo.GetMovies())
                .Returns((List<Movie>)null);

      var controller = new MovieController(_movieRepo.Object, _directorRepo.Object, _mLogger.Object);
      var result = controller.GetMovies();

      Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task PostMovie_ShouldReturOnWithCreateedRecord() {
      var director = new Director() {
        Firstname = "Dawid",
        Lastname = "Domin"
      };

      _directorRepo.Setup(repo => repo.GetDirector(It.IsAny<Guid>()))
                .ReturnsAsync((Director)director);

      var dController = new DirectorController(_directorRepo.Object, _dLogger.Object);
      var dResult = await dController.GetDirector(new Guid());
      var gDirector = (dResult as OkObjectResult).Value as Director;

      var movie = new Movie() {
        DirectorID = gDirector.DirectorID,
        ProductionYear = System.DateTime.Now,
        MovieLength = 1939,
        MovieTitle = "Jak rozpętałem drugą wojnę światową",
      };

      var controller = new MovieController(_movieRepo.Object, _directorRepo.Object, _mLogger.Object);
      var mResult = await controller.PostMovie(movie);
      var createdMovie = (mResult as CreatedAtActionResult).Value as Movie;

      Assert.NotNull(createdMovie);
      Assert.Equal(1939, createdMovie.MovieLength);
      Assert.Equal("Jak rozpętałem drugą wojnę światową", createdMovie.MovieTitle);
    }

    [Fact]
    public async Task DeleteMovie_ShouldReturnNull() {
      var movie = new Movie() {
        DirectorID = new Guid(),
        ProductionYear = System.DateTime.Now,
        MovieLength = 1939,
        MovieTitle = "A movie"
      };
      _movieRepo.Setup(repo => repo.GetMovie(It.IsAny<Guid>())).ReturnsAsync((Movie)movie);

      var controller = new MovieController(_movieRepo.Object, _directorRepo.Object, _mLogger.Object);
      var result = await controller.DeleteMovie(movie.MovieID);
      var deletedMovie = (result as OkObjectResult);

      Assert.Null(deletedMovie);
      Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task UpdateMovie_ShouldRetrunOk() {
      var movie = new Movie() {
        DirectorID = new Guid(),
        ProductionYear = System.DateTime.Now,
        MovieLength = 1939,
        MovieTitle = "A movie"
      };
      var movie2 = new Movie() {
        DirectorID = new Guid(),
        ProductionYear = System.DateTime.Now,
        MovieLength = 1939,
        MovieTitle = "A movie 2"
      };
      _movieRepo.Setup(repo => repo.GetMovie(It.IsAny<Guid>())).ReturnsAsync((Movie)movie);
      _movieRepo.Setup(repo => repo.GetMovieByTitle(It.IsAny<string>())).Returns((Movie)movie);

      var controller = new MovieController(_movieRepo.Object, _directorRepo.Object, _mLogger.Object);
      var result = await controller.UpdateMovie(movie.MovieID, movie2);

      Assert.IsType<OkObjectResult>(result);
    }
  }
}
