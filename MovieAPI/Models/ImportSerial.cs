using System;

namespace MovieAPI.Models {
  public class ImportSerial {
    public Guid SerialID {get; set;}
    public System.DateTime ProductionYear {get; set;}
    public Guid DirectorID {get; set;}
    public int SerialEpisodes {get; set;}
    public string SerialTitle {get; set;}
  }
}