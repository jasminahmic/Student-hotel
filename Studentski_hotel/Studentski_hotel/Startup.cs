using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Studentski_hotel.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DBdata.EntityModels;
using Studentski_hotel.Interface;
using Studentski_hotel.Services;
using ReflectionIT.Mvc.Paging;
using Studentski_hotel.notHub;

namespace Studentski_hotel
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        //[Obsolete]
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<Korisnik>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();
            services.AddRazorPages();
           
            services.AddTransient<IKonkursService, KonkursService>();
            services.AddTransient<IRecepcijaService, RecepcijaService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IAdminService, AdminService>();

            services.AddCloudscribePagination();
            services.AddSignalR();
            services.AddCors();
         
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseCors(options => options.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod());
            //ovo je za vue defaultni gore je za react app
            //app.UseCors(options => options.WithOrigins("http://localhost:8080").AllowAnyHeader().AllowAnyMethod());
            //za angular
            app.UseCors(options => options.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
                endpoints.MapHub<NotHub>("/notHub");

            });
        }
    }
}
