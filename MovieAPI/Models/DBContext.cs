using Microsoft.EntityFrameworkCore;

namespace MovieAPI.Models {
  public class DBContext : DbContext {
    public DbSet<Movie> Movies {get; set;}
    public DbSet<Director> Directors {get; set;}
    public DbSet<Serial> Serials {get; set;}
    public DbSet<MovieSerial> MoviesSerials {get; set;}
    public DBContext(DbContextOptions<DBContext> options): base(options) {
    }
  }
}