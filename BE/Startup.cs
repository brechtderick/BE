using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BE.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using BE.Models;
using Microsoft.EntityFrameworkCore;

namespace BE
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerDocument();
            services.AddDbContext<ArtworkContext>(options => 
            options.UseSqlServer(Configuration.GetConnectionString("ArtworkContext")));
            services.AddCors(options => options.AddPolicy("AllowAllOrigins", builder => builder.AllowAnyOrigin()));
            
            services.AddOpenApiDocument(c =>
            {
                c.DocumentName = "apidocs";
                c.Title = "Artwork API";
                c.Version = "v1";
                c.Description = "The Artwork API documentation description.";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseOpenApi();

            app.UseSwaggerUi3();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("AllowAllOrigins");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
