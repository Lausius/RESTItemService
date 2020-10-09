using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ModelLib.Model;

namespace RESTItemService
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
            services.AddScoped<IItemCatalogService, ItemCatalogService>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Items API",
                    Version = "v1.0",
                    Description = "Example of OpenAPI for api/localItems",
                    //TermsOfService = new Uri("none"),
                    Contact = new OpenApiContact()
                    {
                        Name = "Anders",
                        Email = "ande711e@edu.easj.dk",
                        //Url = new Uri(string.Empty)
                    },
                    License = new OpenApiLicense()
                    {
                        Name = "No license required",
                        //Url = new Uri(string.Empty)
                    }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyOrigin", builder => builder.AllowAnyOrigin());
                options.AddPolicy("AllowMyLocalOrigin", builder => builder.WithOrigins("http://localhost:5000"));
                options.AddPolicy("AllowGetPost", builder => builder.AllowAnyOrigin().WithMethods("GET", "POST", "OPTIONS"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }



            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = "api/help";
            });

            app.UseRouting();
            app.UseCors("AllowMyLocalOrigin");
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
