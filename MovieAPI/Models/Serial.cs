using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace MovieAPI.Models {
  [Table("Serials")]
  public class Serial {
    [Key]
    public Guid SerialID {get; internal set;}

    [Required]
    public System.DateTime ProductionYear {get; set;}

    [Required]
    public Guid DirectorID {get; set;}

    [Required]
    public int SerialEpisodes {get; set;}

    [Required]
    public string SerialTitle {get; set;}
  }
}