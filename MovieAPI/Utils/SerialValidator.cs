using System;
using MovieAPI.Models;
using MovieAPI.Enums;

namespace MovieAPI.Validators {
  public static class SerialValidator {
    public static EValidator CheckSerial(Serial serial) {
      if(serial.DirectorID.ToString().Length < 1) return EValidator.InValid;
      if(!System.DateTime.TryParse(serial.ProductionYear.ToString(), out var dateResult)) return EValidator.InValid;
      if(!int.TryParse(serial.SerialEpisodes.ToString(), out var episodesResult)) return EValidator.InValid;
      if(serial.SerialTitle.Length < 1) return EValidator.InValid;
      if(!Guid.TryParse(serial.SerialID.ToString(), out var guidResult)) return EValidator.InValid;
      return EValidator.Valid;
    }
  }
}