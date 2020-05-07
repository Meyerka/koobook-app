using System;
using KooBooKMVC.Areas.Identity.Data;
using KooBooKMVC.Data;
using KooBooKMVC.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(KooBooKMVC.Areas.Identity.IdentityHostingStartup))]
namespace KooBooKMVC.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<KooBooKMVCContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("KooBooKMVCContextConnection")));

                services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<KooBooKMVCContext>()
                    .AddDefaultUI()
                    .AddDefaultTokenProviders();

                services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>,
                    KoobookUserClaimsFactory>();
                services.AddTransient<IEmailSender, EmailSender>();
            });
        }
    }
}