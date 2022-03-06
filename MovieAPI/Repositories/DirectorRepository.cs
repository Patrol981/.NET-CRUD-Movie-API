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
    var itemsRelated = _dbContext.MoviesSerials.Where(x => x.DirectorID == director.DirectorID).ToList();
    _dbContext.MoviesSerials.RemoveRange(itemsRelated);
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

  public async Task<MovieSerial> GetDirectorWorks(Guid id) {
    var serials = await _dbContext.Serials.FindAsync(id);
    var movies = await _dbContext.Movies.FindAsync(id);
    Console.WriteLine(serials);
    Console.WriteLine(movies);
    return null;
  }
}