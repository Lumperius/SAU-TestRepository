using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using APIGoodMoodProvider.Initializer;
using APIGoodMoodProvider.Options;
using ContextLibrary.DataContexts;
using ContextLibrary.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ModelsLibrary;
using NewsUploader;
using NewsUploader.Interfaces;
using RepositoryLibrary;
using RepositoryLibrary.RepositoryInterface;
using Serilog;
using UserService;
using UserService.Interfaces;
using WorkingLibrary.DataContexts.WorkingUnit;

namespace APIGoodMoodProvider
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
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "GMP API",
                    Version = "v1"
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                x.IncludeXmlComments(xmlPath);
            });

            services.AddScoped<IRepository<User>, UserRepository>();
            services.AddScoped<IRepository<News>, NewsRepository>();

            services.AddTransient<IAdminInitializer, AdminInitializer>();


            var connString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContextPool<DataContext>(options =>
               options.UseSqlServer(connString, b => b.MigrationsAssembly("APIGoodMoodProvider")));


            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options => options.LoginPath = new PathString("/Account/Login"));

            services.AddScoped<DataContext>();
            services.AddScoped<IWorkingUnit, WorkingUnit>();
           
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<IHtmlCleaner, HtmlCleaner>();
            services.AddScoped<IRssLoader, RssLoader>();
            services.AddScoped<INewsParser, NewsParser>(); 
            services.AddScoped<IUserHandler, UserHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            var swaggerOptions = new SwaggerOptions();
            Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);

            app.UseSwagger(options => { options.RouteTemplate = swaggerOptions.JsonRoute; });
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint(swaggerOptions.UIEndpoint, swaggerOptions.Description);
            });

            app.UseAuthorization();
            app.UseAuthorization();

            app.UseSerilogRequestLogging();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=TokenController}/{action=Index}/{id?}");
            });
        }
    }
}
