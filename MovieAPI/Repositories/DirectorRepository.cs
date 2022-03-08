using MovieAPI.Interfaces;
using MovieAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

public class DirectorRepository : IDirectorRepository {
  private readonly DBContext _dbContext;

  public DirectorRepository(DBContext dbContext) {
    _dbContext = dbContext;
  }
  public async Task<Director> AddDirector(Director director) {
    await _dbContext.Directors.AddAsync(director);
    await _dbContext.SaveChangesAsync();
    return director;
  }

  public async void DeleteDirector(Director director) {
    /*
    not sure what's better delete movies and serials if director is deleted or leave it be

    var moviesRelated = _dbContext.Movies.Where(x => x.DirectorID == director.DirectorID).ToList();
    _dbContext.Movies.RemoveRange(moviesRelated);

    var serialsRelated = _dbContext.Serials.Where(x => x.DirectorID == director.DirectorID).ToList();
    _dbContext.Serials.RemoveRange(serialsRelated);
    */

    var directorSerials = _dbContext.SerialDirectors.Where(x => x.DirectorID == director.DirectorID).ToList();
    _dbContext.SerialDirectors.RemoveRange(directorSerials);

    var directorMovies = _dbContext.MovieDirectors.Where(x => x.DirectorID == director.DirectorID).ToList();
    _dbContext.MovieDirectors.RemoveRange(directorMovies);

    _dbContext.Directors.Remove(director);
    await _dbContext.SaveChangesAsync();
  }

  public async Task<Director> GetDirector(Guid id) {
    var director = await _dbContext.Directors.FindAsync(id);
    if(director == null) {
      return null;
    }
    return director;
  }

  public Director GetDirectorByName(string name, string surname) {
    var director = _dbContext.Directors.Where(d => d.Firstname == name).Where(d => d.Lastname == surname).FirstOrDefault();
    if(director == null) {
      return null;
    }
    return director;
  }

  public List<Director> GetDirectors() {
    return _dbContext.Directors.ToList();
  }

  public async Task<Director> UpdateDirector(Guid id, Director director) {
    var existingDirector = await _dbContext.Directors.FindAsync(id);
    if(existingDirector != null) {
      existingDirector.Firstname = director.Firstname;
      existingDirector.Lastname = director.Lastname;
    }
    return director;
  }

  public List<MovieDirector> GetDirectorMovies(Guid id) {
    return _dbContext.MovieDirectors.Where(x => x.DirectorID == id).ToList();
  }

  public List<SerialDirector> GetDirectorSerials(Guid id) {
    return _dbContext.SerialDirectors.Where(x => x.DirectorID == id).ToList();
  }
}