using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace MovieAPI.Models {
  [Table("MovieDirector")]
  public class MovieDirector {
    [Key]
    public Guid InstanceID {get; private init;}
    public Guid DirectorID {get; set;}
    public Guid MovieID {get; set;}
  }
}