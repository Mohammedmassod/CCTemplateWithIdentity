using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TemplateWithIdentity.Models;

namespace TemplateWithIdentity.Data
{
    public class AppDbContext:IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
                
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
           
            var hasher = new PasswordHasher<Users>();
            builder.Entity<Users>().HasData(new Users() { 
            Id= "4a2e1650-21bd-4e67-832e-2e99c267a2e4",
            Name ="رئيس مجلس الادارة",
            UserName="SuperAdmin",
            Email="Admin@gmail.com",
            PhoneNumber="778877887",
            IsClient=true,
            PasswordHash= hasher.HashPassword(null,"Admin123"),
            NormalizedUserName="SUPERADMIN",
            Pass="Admin123",
            NormalizedEmail="ADMIN@GMAIL.COM",
            
                

            });
            builder.Entity<Roles>().HasData(new Roles()
            {
                Name = "SuperAdmin",
                Name_Ar = "مالك النظام",
                NormalizedName = "SUPERADMIN"

            }, new Roles()
            {
                Name = "Admin",
                Name_Ar = "مدير النظام",
                NormalizedName = "ADMIN"
            },
          new Roles()
          {
              Name = "User",
              Name_Ar = "مستخدم",
              NormalizedName = "USER"
          });

            base.OnModelCreating(builder);
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
    }
}
