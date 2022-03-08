using System;

namespace MovieAPI.Models {
  public class ImportMovie {
    public Guid MovieID {get; set;}
    public System.DateTime ProductionYear {get; set;}
    public Guid DirectorID {get; set;}
    public float MovieLength {get; set;}
    public string MovieTitle {get; set;}
  }
}