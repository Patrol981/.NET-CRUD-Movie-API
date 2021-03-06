using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using MovieAPI.Models;

namespace MovieAPI.Interfaces {
  public interface IDirectorRepository {
    List<Director> GetDirectors();
    Task<Director> GetDirector(Guid id);
    Director GetDirectorByName(string name, string surname);
    List<MovieDirector> GetDirectorMovies(Guid id);
    List<SerialDirector> GetDirectorSerials(Guid id);
    Task<Director> AddDirector(Director director);
    void DeleteDirector(Director director);
    Task<Director> UpdateDirector(Guid id, Director director);
  }
}