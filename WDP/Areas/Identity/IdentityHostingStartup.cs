using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WDP.Areas.Identity.Data;
using WDP.Data;

[assembly: HostingStartup(typeof(WDP.Areas.Identity.IdentityHostingStartup))]
namespace WDP.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlite(
                        context.Configuration.GetConnectionString("ApplicationDbContextConnection")));

                services.AddIdentity<ApplicationUser, ApplicationRole>()
                    .AddRoles<ApplicationRole>() // ? 
                    .AddRoleManager<RoleManager<ApplicationRole>>() // ?

                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultUI()
                    .AddDefaultTokenProviders();

                services.AddAuthorization(options =>
                {

                    options.AddPolicy("ViewRoles", policy => policy.RequireClaim("CanViewRoles"));
                    options.AddPolicy("AddRoles", policy => policy.RequireClaim("CanAddRoles"));
                    options.AddPolicy("EditRoles", policy => policy.RequireClaim("CanEditRoles"));
                    options.AddPolicy("DeleteRoles", policy => policy.RequireClaim("CanDeleteRoles"));

                    options.AddPolicy("ViewUsers", policy => policy.RequireClaim("CanViewUsers"));
                    options.AddPolicy("AddUsers", policy => policy.RequireClaim("CanAddUsers"));
                    options.AddPolicy("EditUsers", policy => policy.RequireClaim("CanEditUsers"));
                    options.AddPolicy("DeleteUsers", policy => policy.RequireClaim("CanDeleteUsers"));

                });
            });
        }
    }
}