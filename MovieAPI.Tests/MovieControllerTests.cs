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
    private readonly Mock<IMovieRepository> _repository = new();
    private readonly Mock<ILogger<MovieController>> _logger = new();
    private readonly ITestOutputHelper _output;

    public MovieControllerTests(ITestOutputHelper output) {
      _output = output;
    }
    [Fact]
    public async Task GetMovie_WithUnexistingItem_ReturnsNotFound() {
      _repository.Setup(repo => repo.GetMovie(It.IsAny<Guid>()))
                .ReturnsAsync((Movie)null);

      var controller = new MovieController(_repository.Object, _logger.Object);

      var result = await controller.GetMovie(Guid.NewGuid());

      Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public void GetMovies_ShouldReturnOk() {
      _repository.Setup(repo => repo.GetMovies())
                .Returns((List<Movie>)null);

      var controller = new MovieController(_repository.Object, _logger.Object);
      var result = controller.GetMovies();

      Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task PostMovie_ShouldReturOnWithCreateedRecord() {
      var movie = new Movie() {
        Director = "Damian Dolata",
        ProductionYear = System.DateTime.Now,
        MovieLength = "03:25:00",
        MovieTitle = "Jebane studia",
      };

      var controller = new MovieController(_repository.Object, _logger.Object);

      var result = await controller.PostMovie(movie);

      var createdMovie = (result as CreatedAtActionResult).Value as Movie;
      Assert.NotNull(createdMovie);
      Assert.Equal("Damian Dolata", createdMovie.Director);
      Assert.Equal("03:25:00", createdMovie.MovieLength);
      Assert.Equal("Jebane studia", createdMovie.MovieTitle);
    }
  }
}
