using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using MovieAPI.Models;
using MovieAPI.Interfaces;
using MovieAPI.Repositories;

namespace MovieAPI {
  public class Startup {
    public Startup(IConfiguration configuration) {
    	Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services) {
      services.AddControllers();

      services.AddDbContext<DBContext>( options =>
        options.UseMySQL(Configuration.GetConnectionString("MySQL"))
      );

      services.AddScoped<IMovieRepository, MovieRepository>();
      services.AddScoped<IDirectorRepository, DirectorRepository>();
      services.AddScoped<ISerialRepository, SerialRepository>();
      services.AddScoped<IImportRepoository, ImportRepository>();

      services.AddCors(c => {
        c.AddPolicy("AllowOrigin", options => options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
      });

      services.AddSwaggerGen(c => {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "MovieAPI", Version = "v1" });
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
      if (env.IsDevelopment()) {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MovieAPI v1"));
      }

      if(env.IsProduction()) {
        app.UseHttpsRedirection();
      }

      app.UseCors("AllowOrigin");

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints => {
        endpoints.MapControllers();
      });
    }
  }
}
