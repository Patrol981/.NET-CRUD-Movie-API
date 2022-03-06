using System;
using MovieAPI.Models;
using MovieAPI.Enums;

namespace MovieAPI.Validators {
  public static class DirectorValidator {
    public static EValidator CheckDirector(Director director) {
      if(director.Firstname.ToString().Length < 1) return EValidator.InValid;
      if(director.Lastname.ToString().Length < 1) return EValidator.InValid;
      if(!Guid.TryParse(director.DirectorID.ToString(), out var guidResult)) return EValidator.InValid;
      return EValidator.Valid;
    }
  }
}