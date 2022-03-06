using MovieAPI.Interfaces;
using MovieAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAPI.Repositories {
  public class SerialRepository : ISerialRepository {
    private readonly DBContext _dbContext;

    public SerialRepository(DBContext dbContext) {
      _dbContext = dbContext;
    }
    public async Task<Serial> AddSerial(Serial serial) {
      await _dbContext.Serials.AddAsync(serial);
      await _dbContext.SaveChangesAsync();
      return serial;
    }

    public async void DeleteSerial(Serial serial) {
      var itemsRelated = _dbContext.MoviesSerials.Where(x => x.SerialID == serial.SerialID).ToList();
      _dbContext.MoviesSerials.RemoveRange(itemsRelated);
      _dbContext.Serials.Remove(serial);
      await _dbContext.SaveChangesAsync();
    }

    public async Task<Serial> GetSerial(Guid id) {
      var serial = await _dbContext.Serials.FindAsync(id);
      if(serial == null) {
        return null;
      }
      return serial;
    }

    public Serial GetSerialByTitle(string title) {
      var serial = _dbContext.Serials.SingleOrDefault(x => x.SerialTitle == title);
      if(serial == null) {
        return null;
      }
      return serial;
    }

    public List<Serial> GetSerials() {
      return _dbContext.Serials.ToList();
    }

    public async Task<Serial> UpdateSerial(Guid id, Serial serial) {
      var existingSerial = await _dbContext.Serials.FindAsync(id);
      if(existingSerial != null) {
        existingSerial.Director = serial.Director;
        existingSerial.SerialEpisodes = serial.SerialEpisodes;
        existingSerial.SerialTitle = serial.SerialTitle;
        existingSerial.ProductionYear = serial.ProductionYear;
        _dbContext.Serials.Update(existingSerial);
        await _dbContext.SaveChangesAsync();
      }
      return serial;
    }
  }
}