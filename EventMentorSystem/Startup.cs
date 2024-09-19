using EMS.DB;
using EMS.DB.Repository;
using EMS.DB.Repository.Interface;
using EMS.DB.unitofwork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MudBlazor.Services;
using Microsoft.AspNetCore.ResponseCompression;
using BlazorServerSignalRApp.Server.SignalRHubs;
using System.Linq;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using EMS.DB.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System;

namespace EventMentorSystem
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication("Identity.Application").AddCookie();
            #region Connection String
            services.AddControllers();

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            services.AddDbContext<AppDbContext>(item => item.UseSqlServer(Configuration.GetConnectionString("myconn")));
            // 3 : Cookie Options  
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/Error/AccessDenied";
                options.Cookie.Name = "MyAPP";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.LoginPath = "/";
                //options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.SlidingExpiration = true;
            });
            services.AddHttpContextAccessor();
            #endregion
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddMudServices();
            services.AddSignalRCore();
            services.AddAuthenticationCore();
            services.AddScoped<ProtectedSessionStorage>();
            //services.AddScoped<CustomAuthenticationStateProvider>();
            //services.AddScoped<AuthenticationStateProvider>(provider =>
            //provider.GetRequiredService<CustomAuthenticationStateProvider>());


            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            #region Register service
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IInquiryRepository, InquiryRepository>();
            services.AddScoped<IEventCategoryRepository, EventCategoryRepository>();
            services.AddScoped<ICategoryServiceRepository, CategoryServiceRepository>(); 
            services.AddScoped<IUserRepository, UserRepository>();  
            services.AddScoped<IEventStaffWorkRepository, EventStaffWorkRepository>(); 
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IStaffRepository, StaffRepository>();
            services.AddScoped<INotificationMessagesRepository, NotificationMessagesRepository>();
            services.AddScoped<IOperatorRepository, OperatorRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            #endregion

            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
               RoleManager<IdentityRole> roleManager,
               UserManager<User> userManager)
        {
            app.UseResponseCompression();
            ApplicationDbInitialiser.SeedRoles(roleManager);
            ApplicationDbInitialiser.SeedUsers(userManager);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapHub<ChatHub>("/chathub");
                endpoints.MapFallbackToPage("/_Host");
            });
        }

        //public static class RolesData
        //{
        //    private static readonly string[] Roles = new string[] { "Admin", "Manager", "Member" };

        //    public static async Task SeedRoles(IServiceProvider serviceProvider)
        //    {
        //        using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
        //        {
        //            var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        //            foreach (var role in Roles)
        //            {
        //                if (!await roleManager.RoleExistsAsync(role))
        //                {
        //                    await roleManager.CreateAsync(new IdentityRole(role));
        //                }
        //            }
        //        }
        //    }
        //}
    }
}
