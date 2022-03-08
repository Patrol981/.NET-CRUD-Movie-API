using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace MovieAPI.Models {
  [Table("SerialDirector")]
  public class SerialDirector {
    [Key]
    public Guid InstanceID {get; private init;}
    public Guid DirectorID {get; set;}
    public Guid SerialID {get; set;}
  }
}