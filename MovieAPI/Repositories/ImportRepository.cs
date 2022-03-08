using MovieAPI.Interfaces;
using MovieAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using MovieAPI.Utils;
public class ImportRepository : IImportRepoository {
  private readonly DBContext _dbContext;

  public ImportRepository(DBContext dbContext) {
    _dbContext = dbContext;
  }

  public async Task<ImportDirector> ImportDirector(ImportDirector importDirector) {
    Director director = importDirector.CastToAnother<Director>();
    await _dbContext.Directors.AddAsync(director);
    await _dbContext.SaveChangesAsync();
    return importDirector;
  }

  public async Task<ImportMovie> ImportMovie(ImportMovie importMovie) {
    Movie movie = importMovie.CastToAnother<Movie>();
    var movieDirector = new MovieDirector();
    movieDirector.DirectorID = movie.DirectorID;
    movieDirector.MovieID = movie.MovieID;
    await _dbContext.MovieDirectors.AddAsync(movieDirector);
    await _dbContext.Movies.AddAsync(movie);
    await _dbContext.SaveChangesAsync();
    return importMovie;
  }

  public async Task<ImportSerial> ImportSerial(ImportSerial importSerial) {
    Serial serial = importSerial.CastToAnother<Serial>();
    await _dbContext.Serials.AddAsync(serial);
    var serialDirector = new SerialDirector();
    serialDirector.DirectorID = serial.DirectorID;
    serialDirector.SerialID = serial.SerialID;
    await _dbContext.SerialDirectors.AddAsync(serialDirector);
    await _dbContext.SaveChangesAsync();
    return importSerial;
  }
}