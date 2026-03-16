using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace SentinelGear.Data.Configuration
{
    public static class RoleConfiguration
    {
        public static async Task SeedAsync(IServiceProvider services, IConfiguration config)
        {
            SentinelGearDbContext db = services.GetRequiredService<SentinelGearDbContext>();
            UserManager<IdentityUser> userManager = services.GetRequiredService<UserManager<IdentityUser>>();

            string[] roles = ["Admin", "User"];

            foreach (string role in roles)
            {
                string normalized = role.ToUpperInvariant();

                bool roleExists = await db.Roles.AnyAsync(r => r.NormalizedName == normalized);

                if (!roleExists)
                {
                    await db.Roles.AddAsync(new IdentityRole
                    {
                        Name = role,
                        NormalizedName = normalized
                    });
                }
            }

            await db.SaveChangesAsync();

            string? email = config["Seed:Admin:Email"];
            string? password = config["Seed:Admin:Password"];

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                return;
            }

            string normalizedEmail = email.ToUpperInvariant();

            IdentityUser? admin = await userManager.Users
                .FirstOrDefaultAsync(u => u.NormalizedEmail == normalizedEmail);

            if (admin == null)
            {
                admin = new IdentityUser
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true
                };

                IdentityResult createResult = await userManager.CreateAsync(admin, password);

                if (!createResult.Succeeded)
                {
                    throw new Exception(string.Join(", ", createResult.Errors.Select(e => e.Description)));
                }
            }

            if (!await userManager.IsInRoleAsync(admin, "Admin"))
            {
                IdentityResult roleResult = await userManager.AddToRoleAsync(admin, "Admin");

                if (!roleResult.Succeeded)
                {
                    throw new Exception(string.Join(", ", roleResult.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}