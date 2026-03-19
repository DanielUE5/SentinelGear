using System.Globalization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SentinelGear.Data;
using SentinelGear.Data.Configuration;

namespace SentinelGear;

public class Program
{
    public static async Task Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Низът за свързване 'DefaultConnection' не е намерен.");

        ConfigureCulture();
        ConfigureServices(builder, connectionString);

        WebApplication app = builder.Build();

        ConfigureMiddleware(app);
        ConfigureRoutes(app);
        await ApplyMigrationsAndSeedAsync(app, builder.Configuration);

        await app.RunAsync();
    }

    private static void ConfigureCulture()
    {
        CultureInfo culture = new("en-US");
        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;
    }

    private static void ConfigureServices(WebApplicationBuilder builder, string connectionString)
    {
        builder.Services.AddDbContext<SentinelGearDbContext>(options =>
            options.UseSqlServer(connectionString));

        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services
            .AddDefaultIdentity<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;

                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequiredLength = 10;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<SentinelGearDbContext>();

        builder.Services.AddControllersWithViews();
        builder.Services.AddRazorPages();

        builder.Services.AddDistributedMemoryCache();

        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });
    }

    private static void ConfigureMiddleware(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseStaticFiles();
        app.UseRouting();
        app.UseSession();
        app.UseAuthentication();
        app.UseAuthorization();
    }

    private static void ConfigureRoutes(WebApplication app)
    {
        app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.MapRazorPages();
    }

    private static async Task ApplyMigrationsAndSeedAsync(WebApplication app, IConfiguration configuration)
    {
        using IServiceScope scope = app.Services.CreateScope();
        IServiceProvider services = scope.ServiceProvider;
        SentinelGearDbContext dbContext = services.GetRequiredService<SentinelGearDbContext>();

        await dbContext.Database.MigrateAsync();
        await RoleConfiguration.SeedAsync(services, configuration);
    }
}