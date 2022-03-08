using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace MovieAPI.Models {
  [Table("Movies")]
  public class Movie {
    [Key]
    public Guid MovieID {get; private init;}

    [Required]
    public System.DateTime ProductionYear {get; set;}

    [Required]
    public Guid DirectorID {get; set;}

    [Required]
    public float MovieLength {get; set;}

    [Required]
    public string MovieTitle {get; set;}
  }
}