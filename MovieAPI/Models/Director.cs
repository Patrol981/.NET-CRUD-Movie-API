using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace MovieAPI.Models {
  [Table("Directors")]
  public class Director {
    [Key]
    public Guid DirectorID {get; internal set;}

    [Required]
    public string Firstname {get; set;}

    [Required]
    public string Lastname {get;set;}
  }
}