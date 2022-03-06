using MovieAPI.Interfaces;
using MovieAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAPI.Repositories {
  public class MovieRepository : IMovieRepository {
    private readonly DBContext _dbContext;
    public MovieRepository(DBContext dbContext) {
      _dbContext = dbContext;
    }
    public List<Movie> GetMovies() {
      return _dbContext.Movies.ToList();
    }

    public async Task<Movie> GetMovie(Guid id) {
      var movie = await _dbContext.Movies.FindAsync(id);
      if(movie == null) {
        return null;
      }
      return movie;
    }

    public Movie GetMovieByTitle(string title) {
      var movie = _dbContext.Movies.SingleOrDefault(x => x.MovieTitle == title);
      if(movie == null) {
        return null;
      }
      return movie;
    }

    public async Task<Movie> AddMovie(Movie movie) {
      await _dbContext.Movies.AddAsync(movie);
      await _dbContext.SaveChangesAsync();
      return movie;
    }

    public async void DeleteMovie(Movie movie) {
      var itemsRelated = _dbContext.MoviesSerials.Where(x => x.MovieID == movie.MovieID).ToList();
      _dbContext.MoviesSerials.RemoveRange(itemsRelated);
      _dbContext.Movies.Remove(movie);
      await _dbContext.SaveChangesAsync();
    }

    public async Task<Movie> UpdateMovie(Guid id, Movie movie) {
      var existingMovie = await _dbContext.Movies.FindAsync(id);
      if(existingMovie != null) {
        existingMovie.Director = movie.Director;
        existingMovie.MovieLength = movie.MovieLength;
        existingMovie.MovieTitle = movie.MovieTitle;
        existingMovie.ProductionYear = movie.ProductionYear;
        _dbContext.Movies.Update(existingMovie);
        await _dbContext.SaveChangesAsync();
      }
      return movie;
    }
  }
}