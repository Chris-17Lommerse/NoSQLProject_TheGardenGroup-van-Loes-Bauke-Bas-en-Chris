using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System.Threading.Tasks;
using Ticket_System_TheGardenGroup_With_Identity_Framework.Data;
using Ticket_System_TheGardenGroup_With_Identity_Framework.Models.Enums;
using Ticket_System_TheGardenGroup_With_Identity_Framework.Repositories;
using Ticket_System_TheGardenGroup_With_Identity_Framework.Repositories.Interfaces;
using Ticket_System_TheGardenGroup_With_Identity_Framework.Services;
using Ticket_System_TheGardenGroup_With_Identity_Framework.Services.Interfaces;

namespace Ticket_System_TheGardenGroup_With_Identity_Framework
{
    public class Program
    {
        public static async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            foreach(var roleName in Enum.GetNames(typeof(EmployeeRole)))
            {
                var roleExists = await roleManager.RoleExistsAsync(roleName);
                if(!roleExists)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }

        public static async Task AssignDefaultRoleToExistingUser(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            var users = userManager.Users.ToList();

            foreach(var user in users)
            {
                var roles = await userManager.GetRolesAsync(user);
                if(!roles.Any())
                {
                    await userManager.AddToRoleAsync(user, EmployeeRole.Regular_Employee.ToString());
                }
            }
        }

        public static async Task Main(string[] args)
        {
            DotNetEnv.Env.TraversePath().Load();

            var builder = WebApplication.CreateBuilder(args);

            var mongoConnectionString = Environment.GetEnvironmentVariable("Mongo__ConnectionString");
            var databaseName = Environment.GetEnvironmentVariable("Mongo__Database") ?? "TheGardenGroup";

            if (string.IsNullOrWhiteSpace(mongoConnectionString))
                throw new InvalidOperationException("Mongo__ConnectionString is niet ingesteld in .env");

            builder.Services.AddSingleton<IMongoClient>(sp =>
            {
                return new MongoClient(mongoConnectionString);
            });

            builder.Services.AddScoped(sp =>
            {
                var client = sp.GetRequiredService<IMongoClient>();
                return client.GetDatabase(databaseName);
            });

            //Moeten dit geen "AddSingleton<>" zijn?
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<ITicketRepository, TicketRepository>();

            builder.Services.AddScoped<ITicketService, TicketService>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddRazorPages();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            using(var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                await CreateRoles(services);
                await AssignDefaultRoleToExistingUser(services);
            }

            app.Run();
        }
    }
}
