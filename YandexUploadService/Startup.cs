using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using YandexUploadService.Data.Entities;
using YandexUploadService.Data.Repository;
using YandexUploadService.Data.Repository.Base;
using YandexUploadService.Services;
using YandexUploadService.Services.Base;
using Microsoft.AspNetCore.Http;
using System.Reflection;
using System;
using Microsoft.AspNetCore.SpaServices.AngularCli;

namespace YandexUploadService
{
    public class Startup
    {
        private readonly IWebHostEnvironment env;

        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            this.env = env;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/test-app";
            });

            //services.AddScoped<ILogger, ILogger>();
            services.AddScoped<IS3Service, S3Service>();
            services.AddScoped<IFilesService, FilesService>();

            // --- Data Base ---
            services.AddScoped<IDataRepository<Person>, PersonsRepository>();
            services.AddScoped<IDataRepository<Key>, KeysRepository>();


            if (env.IsDevelopment())
            {
                services.AddDbContext<ApplicationDbContext>();
            }
            else
            {
                services.AddHttpsRedirection(options =>
                {
                    options.RedirectStatusCode = StatusCodes.Status308PermanentRedirect;
                    options.HttpsPort = 443;
                });

                services.AddDbContext<ApplicationDbContext>(options => {
                    options.EnableSensitiveDataLogging();
                //    options.UseNpgsql(Configuration.GetConnectionString("RemoteDatabase"));
                }
                // options.UseInMemoryDatabase("TestDB")
                );

            }

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "YandexUploadService", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "YandexUploadService API V2");

                //var assembly = GetType().GetTypeInfo().Assembly;
                //var ns = assembly.GetName().Name;
                //c.IndexStream = () => assembly.GetManifestResourceStream($"{ns}.index.html");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors(x => x
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                );
            }

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";
                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
                }
            });
        }
    }
}
