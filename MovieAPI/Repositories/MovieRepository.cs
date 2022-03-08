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
      var movieDirector = new MovieDirector();
      movieDirector.DirectorID = movie.DirectorID;
      movieDirector.MovieID = movie.MovieID;
      await _dbContext.MovieDirectors.AddAsync(movieDirector);
      await _dbContext.SaveChangesAsync();
      return movie;
    }

    public async void DeleteMovie(Movie movie) {
      var moviesRelated = _dbContext.MovieDirectors.Where(x => x.MovieID == movie.MovieID).ToList();
      _dbContext.MovieDirectors.RemoveRange(moviesRelated);
      _dbContext.Movies.Remove(movie);
      await _dbContext.SaveChangesAsync();
    }

    public async Task<Movie> UpdateMovie(Guid id, Movie movie) {
      var existingMovie = await _dbContext.Movies.FindAsync(id);
      if(existingMovie != null) {

        var existingMovieDirector = _dbContext.MovieDirectors.Where
          (x => x.MovieID == existingMovie.MovieID).FirstOrDefault();
        existingMovieDirector.DirectorID = movie.DirectorID;
        _dbContext.MovieDirectors.Update(existingMovieDirector);

        existingMovie.DirectorID = movie.DirectorID;
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