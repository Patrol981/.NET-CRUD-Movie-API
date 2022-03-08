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
      var serialDirector = new SerialDirector();
      serialDirector.DirectorID = serial.DirectorID;
      serialDirector.SerialID = serial.SerialID;
      await _dbContext.SerialDirectors.AddAsync(serialDirector);
      await _dbContext.SaveChangesAsync();
      return serial;
    }

    public async void DeleteSerial(Serial serial) {
      var serialsRelated = _dbContext.SerialDirectors.Where(x => x.SerialID == serial.SerialID).ToList();
      _dbContext.SerialDirectors.RemoveRange(serialsRelated);
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

        var existingSerialDirector = _dbContext.SerialDirectors.Where
          (x => x.SerialID == existingSerial.SerialID).FirstOrDefault();
        existingSerialDirector.DirectorID = serial.DirectorID;
        _dbContext.SerialDirectors.Update(existingSerialDirector);

        existingSerial.DirectorID = serial.DirectorID;
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