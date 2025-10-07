using MongoDB.Driver;
using Ticket_System_TheGardenGroup.Repositories.Interfaces;
using Ticket_System_TheGardenGroup.Repositories;
using Ticket_System_TheGardenGroup.Services.Interfaces;
using Ticket_System_TheGardenGroup.Services;

namespace Ticket_System_TheGardenGroup
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Laad .env vóór configuratie
            DotNetEnv.Env.TraversePath().Load();

            var builder = WebApplication.CreateBuilder(args);

            // Lees connectiegegevens
            var mongoConnectionString = Environment.GetEnvironmentVariable("Mongo__ConnectionString");
            var databaseName = Environment.GetEnvironmentVariable("Mongo__Database") ?? "TheGardenGroup"; // fallback

            if (string.IsNullOrWhiteSpace(mongoConnectionString))
                throw new InvalidOperationException("Mongo__ConnectionString is niet ingesteld in .env");

            // MongoClient als Singleton
            builder.Services.AddSingleton<IMongoClient>(sp =>
            {
                return new MongoClient(mongoConnectionString);
            });

            // 2. Database als Scoped service
            builder.Services.AddScoped(sp =>
            {
                var client = sp.GetRequiredService<IMongoClient>();
                return client.GetDatabase(databaseName);
            });

            // 3. Repositories
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<ITicketRepository, TicketRepository>();

            // 4. Services
            builder.Services.AddScoped<ITicketService, TicketService>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();

            // 5. MVC controllers
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
