using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using MovieAPI.Models;

namespace MovieAPI.Interfaces {
  public interface IMovieRepository {
    List<Movie> GetMovies();
    Task<Movie> GetMovie(Guid id);
    Movie GetMovieByTitle(string title);
    Task<Movie> AddMovie(Movie movie);
    void DeleteMovie(Movie movie);
    Task<Movie> UpdateMovie(Guid id, Movie movie);
  }
}