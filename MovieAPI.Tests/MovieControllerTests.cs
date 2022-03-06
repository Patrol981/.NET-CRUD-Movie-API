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
<<<<<<< HEAD
        MovieTitle = "Piwo",
=======
        MovieTitle = "siema",
>>>>>>> 68b78d67d93743c6297e5accd2bc54ecf6e88df3
      };

      var controller = new MovieController(_repository.Object, _logger.Object);

      var result = await controller.PostMovie(movie);

      var createdMovie = (result as CreatedAtActionResult).Value as Movie;
      Assert.NotNull(createdMovie);
      Assert.Equal("Damian Dolata", createdMovie.Director);
      Assert.Equal("03:25:00", createdMovie.MovieLength);
<<<<<<< HEAD
      Assert.Equal("Piwo", createdMovie.MovieTitle);
    }

    [Fact]
    public async Task UpdateMovie_OnEdit_ShouldReturnDiffrentData() {
      var movie = new Movie() {
        Director = "Damian Dolata",
        ProductionYear = System.DateTime.Now,
        MovieLength = "03:25:00",
        MovieTitle = "Piwo",
      };
      var movieUpdate = new Movie() {
        Director = "Kacper Adler",
        ProductionYear = System.DateTime.MaxValue,
        MovieLength = "03:00:00",
        MovieTitle = "Volkswagen Polo",
      };

      _repository.Setup(repo => repo.GetMovie(It.IsAny<Guid>()))
                 .ReturnsAsync(movie);

      var movieID = movie.MovieID;

      var controller = new MovieController(_repository.Object, _logger.Object);


      var updateResult = await controller.UpdateMovie(movieID, movieUpdate);


      Assert.IsType<OkObjectResult>(updateResult);
      Assert.NotNull(updateResult);

=======
      Assert.Equal("siema", createdMovie.MovieTitle);
>>>>>>> 68b78d67d93743c6297e5accd2bc54ecf6e88df3
    }
  }
}
