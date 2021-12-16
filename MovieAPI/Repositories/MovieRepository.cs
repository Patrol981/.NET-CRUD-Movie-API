using MovieAPI.Interfaces;
using MovieAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MovieAPI.Repositories {
  public class MovieRepository: IMovieRepository {
    private readonly MovieContext _movieContext;
    public MovieRepository(MovieContext movieContext) {
      _movieContext = movieContext;
    }
    public List<Movie> GetMovies() {
      return _movieContext.Movies.ToList();
    }

    public async Task<Movie> GetMovie(Guid id) {
      var movie = await _movieContext.Movies.FindAsync(id);
      if(movie == null) {
        return null;
      }
      return movie;
    }

    public Movie GetMovieByTitle(string title) {
      var movie = _movieContext.Movies.SingleOrDefault(x => x.MovieTitle == title);
      if(movie == null) {
        return null;
      }
      return movie;
    }

    public async Task<Movie> AddMovie(Movie movie) {
      await _movieContext.Movies.AddAsync(movie);
      await _movieContext.SaveChangesAsync();
      return movie;
    }

    public async void DeleteMovie(Movie movie) {
      _movieContext.Movies.Remove(movie);
      await _movieContext.SaveChangesAsync();
    }

    public async Task<Movie> UpdateMovie(Guid id, Movie movie) {
      var existingMovie = await _movieContext.Movies.FindAsync(id);
      if(existingMovie != null) {
        existingMovie.Director = movie.Director;
        existingMovie.MovieLength = movie.MovieLength;
        existingMovie.MovieTitle = movie.MovieTitle;
        existingMovie.ProductionYear = movie.ProductionYear;
        _movieContext.Movies.Update(existingMovie);
        await _movieContext.SaveChangesAsync();
      }
      return movie;
    }
  }
}