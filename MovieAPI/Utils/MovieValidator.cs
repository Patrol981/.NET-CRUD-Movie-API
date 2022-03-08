using System;
using MovieAPI.Models;
using MovieAPI.Enums;

namespace MovieAPI.Validators {
  public static class MovieValidator {
    public static EValidator CheckMovie(Movie movie) {
      if(movie.DirectorID.ToString().Length < 1) return EValidator.InValid;
      if(!System.DateTime.TryParse(movie.ProductionYear.ToString(), out var dateResult)) return EValidator.InValid;
      if(!float.TryParse(movie.MovieLength.ToString(), out var lenghtResult)) return EValidator.InValid;
      if(movie.MovieTitle.Length < 1) return EValidator.InValid;
      if(!Guid.TryParse(movie.MovieID.ToString(), out var guidResult)) return EValidator.InValid;
      return EValidator.Valid;
    }
  }
}