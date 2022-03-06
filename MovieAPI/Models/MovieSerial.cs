using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace MovieAPI.Models {
  [Table("MovieSerial")]
  public class MovieSerial {
    [Key]
    public Guid DirectorID {get; set;}
    public Guid MovieID {get; set;}
    public Guid SerialID {get; set;}
  }
}