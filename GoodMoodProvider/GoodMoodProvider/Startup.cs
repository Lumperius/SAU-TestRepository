using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using GoodMoodProvider.DbInitializer;
using Microsoft.Extensions.Logging;
using NewsUploader;
using NewsUploader.Interfaces;
using RepositoryLibrary.RepositoryInterface;
using ModelsLibrary;
using RepositoryLibrary;
using ContextLibrary.DataContexts;
using ContextLibrary.Interfaces;
using WorkingLibrary.DataContexts.WorkingUnit;
using GoodMoodProvider.DbInitializer.Interfaces;
using Serilog;

namespace GoodMoodProvider
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
            services.AddControllersWithViews();

            services.AddScoped<IRepository<User>, UserRepository>();
            services.AddScoped<IRepository<News>, NewsRepository>();

            services.AddTransient<IAdminInitializer, AdminInitializer>();


            var connString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContextPool<DataContext>(options =>
            options.UseSqlServer(connString));

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options => options.LoginPath = new PathString("/Account/Login"));

            services.AddScoped<DataContext>();
            services.AddScoped<IWorkingUnit, WorkingUnit>();

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
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSerilogRequestLogging();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            
        }
    }
}
