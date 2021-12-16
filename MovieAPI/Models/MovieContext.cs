using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieAPI.Models {
  public class MovieContext: DbContext {
    public DbSet<Movie> Movies {get; set;}
    public MovieContext(DbContextOptions<MovieContext> options): base(options) {
    }
  }
}