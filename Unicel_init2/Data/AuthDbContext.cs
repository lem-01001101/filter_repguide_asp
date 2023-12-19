using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Unicel_init2.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // seed roles (user, admin, superadmin)
            var adminRoleId = "7717c4ed-ef16-48e9-ad66-3cffcfdeb1b8";
            var superAdminRoleId = "19009310-db28-4870-9f18-7c7fc668dcfb";
            var userRoleId = "98660fc9-e111-424f-baeb-76b6657986f8";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId
                },
                new IdentityRole
                {
                    Name = "SuperAdmin",
                    NormalizedName = "SuperAdmin",
                    Id = superAdminRoleId,
                    ConcurrencyStamp = superAdminRoleId
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "User",
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);

            // seed superadminuser

            var superAdminId = "8bee75d8-be59-47f6-bda5-3e36b36a7205";
            var superAdminUser = new IdentityUser
            {
                UserName = "superadmin@unicel.com",
                Email = "superadmin@unicel.com ",
                NormalizedEmail = "superadmin@unicel.com".ToUpper(),
                NormalizedUserName = "superadmin@unicel.com".ToUpper(),
                Id = superAdminId
            };

            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(superAdminUser
                , "Superadmin@123");

            builder.Entity<IdentityUser>().HasData(superAdminUser);

            // add all roles to superadminuser
            var superAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = superAdminRoleId,
                    UserId = superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = userRoleId,
                    UserId = superAdminId
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);
        }
    }
}
