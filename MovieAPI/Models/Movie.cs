using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace MovieAPI.Models {
  [Table("Movies")]
  public class Movie {
    [Key]
    public Guid MovieID {get; init;}

    [Required]
    public System.DateTime ProductionYear {get; set;}

    [Required]
    public string Director {get; set;}

    [Required]
    public string MovieLength {get; set;}

    [Required]
    public string MovieTitle {get; set;}
  }
}