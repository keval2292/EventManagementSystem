using System;
using EMS.DB;
using EMS.DB.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(EventMentorSystem.Areas.Identity.IdentityHostingStartup))]
namespace EventMentorSystem.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        //public void Configure(IWebHostBuilder builder)
        //{
        //    builder.ConfigureServices((context, services) => {
        //    });
        //}
        public IdentityHostingStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {

                // Inject Database
                //var builder = new NpgsqlConnectionStringBuilder(CrmtConfiguration.PostgreSql.ConnectionString)
                //{
                //    Password = CrmtConfiguration.PostgreSql.DbPassword
                //};

                services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("myconn")));

                services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()  // <------
                .AddEntityFrameworkStores<AppDbContext>();
            });
        }
    }
}