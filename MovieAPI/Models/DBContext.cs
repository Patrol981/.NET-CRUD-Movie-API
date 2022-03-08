using Microsoft.EntityFrameworkCore;

namespace MovieAPI.Models {
  public class DBContext : DbContext {
    public DbSet<Movie> Movies {get; set;}
    public DbSet<Director> Directors {get; set;}
    public DbSet<Serial> Serials {get; set;}
    public DbSet<MovieDirector> MovieDirectors {get; set;}
    public DbSet<SerialDirector> SerialDirectors {get; set;}
    public DBContext(DbContextOptions<DBContext> options): base(options) {
    }
  }
}