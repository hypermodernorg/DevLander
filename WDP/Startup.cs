using DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WDP.Areas.Identity.Data;

namespace WDP
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
            services.AddRazorPages()
                .AddRazorRuntimeCompilation();
            services.AddAntiforgery(o => o.HeaderName = "WDP-TOKEN");
            services.AddDbContext<PuzzleContext>(options =>
                            options.UseSqlite(
                                Configuration.GetConnectionString("PuzzleDbContextConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
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

            app.Use(async (context, next) =>
            {
                var url = context.Request.Path.Value;

                // Rewrite to index
                if (url == "" || url == "/")
                {
                        context.Request.Path = "/puzzles/landingpage/";
                }
                await next();
            });



            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            MyIdentityDataInitializer.SeedData(userManager, roleManager);
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapAreaControllerRoute(
                    areaName: "areas",
                    name: "areas",
                    pattern: "{area:exists}/{Controller=Default}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Puzzles}/{action=LandingPage}/{id?}");
            });
        }
    }

    public static class MyIdentityDataInitializer
    {
        public static void SeedData (UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        public static void SeedUsers (UserManager<ApplicationUser> userManager)
        {
            //if (userManager.FindByEmailAsync("lander083077@gmail.com").Result == null)
            if (userManager.FindByNameAsync("Admin").Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = "Admin";
                user.Email = "lander083077@gmail.com";

                IdentityResult result = userManager.CreateAsync(user, "Hypermodern1!h").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Administrator").Wait();
                }
            }

        }

        public static void SeedRoles (RoleManager<ApplicationRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Administrator").Result)
            {
                ApplicationRole role = new ApplicationRole();
                role.Name = "Administrator";
                role.Description = "Perform all admin functions.";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("MemberPlus").Result)
            {
                ApplicationRole role = new ApplicationRole();
                role.Name = "MemberPlus";
                role.Description = "Paid Membership.";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Member").Result)
            {
                ApplicationRole role = new ApplicationRole();
                role.Name = "Member";
                role.Description = "Basic Membership.";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Guest").Result)
            {
                ApplicationRole role = new ApplicationRole();
                role.Name = "Guest";
                role.Description = "Guest User.";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
        }
    }
}
