using Microsoft.EntityFrameworkCore;
using SolutionOrders.API.Models.Data;
using System.Reflection;
using Mapster;
using SolutionOrders.API.Features.Items.Providers;
using SolutionOrders.API.Features.Items.Services;

namespace SolutionOrders.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            InitializeServicesAndDbContext(builder);
            SetUpCorsPolicyForDevelopment(builder);

            var app = builder.Build();
            InitializeAutomaticMigrations(app);
            InitializeDevelopmentEnvironment(app);

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }

        private static void SetUpCorsPolicyForDevelopment(WebApplicationBuilder builder)
        {
            if (builder.Environment.IsDevelopment())
            {
                builder.Services.AddCors(options =>
                {
                    options.AddPolicy("AllowAll",
                        policy => policy
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader());
                });
            }
        }

        private static void InitializeServicesAndDbContext(WebApplicationBuilder builder)
        {
            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            // DbContext
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDbContext")));
            // MediatR
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            
            // Providers
            builder.Services.AddScoped<IItemProvider, ItemProvider>();
            
            // Services
            builder.Services.AddScoped<IItemService, ItemService>();
            
            // Mapster
            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
        }

        private static void InitializeDevelopmentEnvironment(WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseCors("AllowAll");
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/openapi/v1.json", "v1");
                });
            }
        }

        private static void InitializeAutomaticMigrations(WebApplication app)
        {
            var retryCount = 5;
            using var scope = app.Services.CreateScope();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            for (int i = 0; i < retryCount; i++)
            {
                try
                {
                    dbContext.Database.Migrate();
                    logger.LogInformation("Database migrations applied successfully.");
                    return;
                }
                catch (Exception ex)
                {
                    logger.LogWarning(ex, "Migration attempt {Attempt}/{MaxRetries} failed. Retrying in 5s...", i + 1, retryCount);
                    Task.Delay(5000);
                }
            }

            logger.LogError("Could not apply migrations after {MaxRetries} attempts.", retryCount);
        }
    }
}