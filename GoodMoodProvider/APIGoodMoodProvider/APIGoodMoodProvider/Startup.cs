using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.IO;
using APIGoodMoodProvider.Initializer;
using ContextLibrary.DataContexts;
using Hangfire;
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
using Swashbuckle.AspNetCore.Swagger;

namespace APIGoodMoodProvider
{
    public class Startup
    {
        private readonly INewsService _newsHandler;
        public Startup(IConfiguration configuration, INewsService newsService)
        {
            Configuration = configuration;
            _newsHandler = newsService;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo() { Title = "GMP API", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, "XmlDocumentation", xmlFile);
                x.IncludeXmlComments(xmlPath);
            });

            services.AddScoped<IRepository<User>, UserRepository>();
            services.AddScoped<IRepository<News>, NewsRepository>();


            var connString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContextPool<DataContext>(options =>
            options.UseSqlServer(connString));

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options => options.LoginPath = new PathString("/AccountMVC/Login"));

            services.AddTransient<IAdminInitializer, AdminInitializer>();

            services.AddScoped< DataContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IHtmlCleaner, HtmlCleaner>();
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<IRssLoader, RssLoader>();
            services.AddScoped<INewsParser, NewsParser>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHangfireServer();
            app.UseHangfireDashboard();

            app.UseSerilogRequestLogging();

            app.UseHttpsRedirection();

            app.UseRouting();

            var swaggerOptions = new SwaggerOptions();
            Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);

            app.UseSwagger(options => { options.RouteTemplate = "swagger/swagger.json"/*swaggerOptions.JsonRoute*/; });
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("v1/swagger.json"/*swaggerOptions.UIEndpoint*/, "GMPjson" /*swaggerOptions.Description*/);
            });

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=TokenController}/{action=Index}/{id?}");
            });

          //  RecurringJob.AddOrUpdate(
          //      () => _newsHandler.LoadNewsInDb(),
          //      Cron.Hourly);

        }
    }
}
