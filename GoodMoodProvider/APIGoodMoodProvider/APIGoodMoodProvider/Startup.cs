using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.IO;
using APIGoodMoodProvider.Initializer;
using APIGoodMoodProvider.Options;
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
using Microsoft.OpenApi.Models;
using ModelsLibrary;
using NewsUploader;
using NewsUploader.Interfaces;
using RepositoryLibrary;
using RepositoryLibrary.RepositoryInterface;
using Serilog;
using UserService;
using UserService.Interfaces;
using MediatR;

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
            services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
           
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo() { Title = "GMP API", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                x.IncludeXmlComments(xmlPath);
            });

            services.AddScoped<IRepository<User>, UserRepository>();
            services.AddScoped<IRepository<News>, NewsRepository>();
            services.AddScoped<IRepository<Role>, RoleRepository>();
            services.AddScoped<IRepository<UserRole>, UserRoleRepository>();

            var connString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContextPool<DataContext>(options =>
            options.UseSqlServer(connString));


            services.AddHangfire(config =>
            {
                config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseDefaultTypeSerializer()
                .UseSqlServerStorage(connString);
            });

            services.AddCors();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options => options.LoginPath = new PathString("/AccountMVC/Login"));

            services.AddTransient<IAdminInitializer, AdminInitializer>();

            services.AddScoped<DataContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
          
            var assembly = AppDomain.CurrentDomain.Load("CqsLibrary");
            services.AddMediatR(assembly);
           
            services.AddScoped<IEncrypter, Encrypter>();
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<IHtmlCleaner, HtmlCleaner>();
            services.AddScoped<IRssLoader, RssLoader>();
            services.AddScoped<INewsRater, NewsRater>();
            services.AddScoped<INewsParser, NewsParser>();
            services.AddScoped<IUserHandler, UserHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.FirstOrDefault(sc => sc.ServiceType == typeof(NewsService));
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseHangfireServer();
            app.UseHangfireDashboard();

            app.UseSerilogRequestLogging();

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(builder => builder.AllowAnyOrigin());

            var swaggerOptions = new Options.SwaggerOptions();
            Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint(swaggerOptions.UIEndpoint, swaggerOptions.Description);
            });

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=TokenController}/{action=Index}/{id?}");
            });


        }
    }
}
