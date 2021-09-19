using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApp_BackOffice.Areas.Identity.Data;
using WebApp_BackOffice.Data;

[assembly: HostingStartup(typeof(WebApp_BackOffice.Areas.Identity.IdentityHostingStartup))]
namespace WebApp_BackOffice.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<BackOfficeDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("BackOfficeDbContextConnection")));

                services.AddDefaultIdentity<BackOfficeUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<BackOfficeDbContext>();
            });
        }
    }
}