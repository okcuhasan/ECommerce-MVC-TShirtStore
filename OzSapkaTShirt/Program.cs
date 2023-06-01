using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OzSapkaTShirt.Data;
using Microsoft.AspNetCore.Identity;
using OzSapkaTShirt.Models;
using OzSapkaTShirt2.Data;

namespace OzSapkaTShirt

{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ApplicationContext context;

            builder.Services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationContext") ?? throw new InvalidOperationException("Connection string 'ApplicationContext' not found.")));

            builder.Services.AddDefaultIdentity<ApplicationUser>(options => { options.SignIn.RequireConfirmedAccount = false; options.Password.RequireNonAlphanumeric = false; })
                .AddEntityFrameworkStores<ApplicationContext>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddSession();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();
                        app.UseAuthentication();;

            app.UseAuthorization();
            app.UseSession();

            app.MapControllerRoute(
              name: "areas",
              pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
            );
            app.MapControllerRoute(
                name: "default",    
                pattern: "{controller=Home}/{action=Index}/{id?}");
            context = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope().ServiceProvider.GetService<ApplicationContext>();
            context.Database.Migrate();
            EnsureCreated ensureCreated = new EnsureCreated(context);

            app.Run();
        }
    }
}