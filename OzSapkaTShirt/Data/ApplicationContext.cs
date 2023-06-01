using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OzSapkaTShirt.Models;

namespace OzSapkaTShirt.Data
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext (DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<OzSapkaTShirt.Models.Product> Products { get; set; } = default!;
        public DbSet<OzSapkaTShirt.Models.Gender> Genders { get; set; } = default!;
        public DbSet<OzSapkaTShirt.Models.City> Cities { get; set; } = default!;
        public DbSet<OzSapkaTShirt.Models.Order> Orders { get; set; } = default!;
        public DbSet<OzSapkaTShirt.Models.OrderProduct> OrderProducts { get; set; } = default!;


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<OrderProduct>().HasKey(o => new { o.OrderId, o.ProductId });
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
