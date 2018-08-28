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
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using keep.Models;
using Swashbuckle.AspNetCore.Swagger;
using keep.Services;
using keep.Contracts;
using keep.Data;

namespace keep
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment currentEnvironment)
        {
            Configuration = configuration;
            _currentEnvironment = currentEnvironment;
        }

        public IHostingEnvironment _currentEnvironment { get; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //var connection = @"Server=db;Database=master;User=sa;Password=Shashi_1996;";

            services.AddCors(
                options => options.AddPolicy("AllowSpecificOrigin",
                builder => builder.WithOrigins("http://localhost:4200"))
                );

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //services.Configure<Settings>(options =>
            //{
            //    options.ConnectionString = Configuration.GetSection("MongoDb:ConnectionString").Value;
            //    options.Database = Configuration.GetSection("MongoDb:Database").Value;
            //});

            // Adding service for SWAGGER
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "First API using SWAGGER", Version = "v1" });
            });

            services.AddTransient<IKeepService, KeepService>();
            services.AddTransient<IDataContext, DataContext>();

            //services.AddDbContext<keepContext>(options =>
            //       options.UseSqlServer(Configuration.GetConnectionString("keepContext")));



            if (_currentEnvironment.IsEnvironment("Testing"))
            {

                services.Configure<Settings>(options =>
                {
                    options.ConnectionString = Configuration.GetSection("MongoDb:ConnectionString").Value;
                    options.Database = Configuration.GetSection("MongoDb:Database").Value;
                });
            }
            else
            {
                //services.AddDbContext<keepContext>(options =>
                //options.UseSqlServer(Configuration.GetConnectionString("keepContext"), dbOptions => dbOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null)));
                //services.AddDbContext<keepContext>(options => options.UseSqlServer(connection));
                services.Configure<Settings>(options =>
                {
                    options.ConnectionString = Configuration.GetSection("MongoDb:ConnectionString").Value;
                    options.Database = Configuration.GetSection("MongoDb:Database").Value;
                });
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors("AllowSpecificOrigin");

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "First API using SWAGGER V1");
                //c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();
            app.UseMvc();



            //context.Database.Migrate();
        }
    }
}
