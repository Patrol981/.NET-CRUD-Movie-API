using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using MovieAPI.Models;

namespace MovieAPI.Interfaces {
  public interface IImportRepoository {
    Task<ImportDirector> ImportDirector(ImportDirector importTirector);
    Task<ImportMovie> ImportMovie(ImportMovie importMovie);
    Task<ImportSerial> ImportSerial(ImportSerial importSerial);
  }
}