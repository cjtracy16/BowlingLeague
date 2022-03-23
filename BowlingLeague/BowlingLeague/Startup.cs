using BowlingLeague.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
//using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingLeague
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

            services.AddDbContext<BowlersDbContext>(options =>
            {
                options.UseMySql(Configuration["ConnectionStrings:BowlersDbConnection"]);
            });

            //services.AddIdentity<IdentityUser, IdentityRole>()
            //    .AddEntityFrameworkStores<AppIdentityDBContext>();

            services.AddScoped<IBowlersRepository, EFBowlersRepository>();
            services.AddScoped<ITeamsRepository, EFTeamsRepository>();

            services.AddRazorPages();

            services.AddDistributedMemoryCache();

            services.AddSession();

            //services.AddScoped<Cart>(x => SessionCart.GetCart(x));

            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddServerSideBlazor();
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
            //app.UseSession();
            app.UseRouting();

            //app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //Both TeamName and PageNum
                endpoints.MapControllerRoute(
                    name: "TeamPage",
                    pattern: "{teamID}/Page{pageNum}",
                    defaults: new { Controller = "Home", action = "Index"
                    });

                //PageNum Only
                endpoints.MapControllerRoute(
                    name: "Paging",
                    pattern: "Page{pageNum}",
                    defaults: new { Controller = "Home", action = "Index", pageNum = 1
                    });

                //TeamName Only
                endpoints.MapControllerRoute(
                    name: "Team",
                    pattern: "TeamID{teamID}",
                    defaults: new { Controller = "Home", action = "Index", pageNum = 1
                    });

                //Home/Default Page
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();

                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("admin/{*catchall}", "/Admin/Index");
            });

            //IdentitySeedData.EnsurePopulated(app);
        }
    }
}
