using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using MovieAPI.Models;

namespace MovieAPI.Interfaces {
  public interface ISerialRepository {
    List<Serial> GetSerials();
    Task<Serial> GetSerial(Guid id);
    Serial GetSerialByTitle(string title);
    Task<Serial> AddSerial(Serial serial);
    void DeleteSerial(Serial serial);
    Task<Serial> UpdateSerial(Guid id, Serial serial);
  }
}