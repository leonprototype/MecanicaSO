using Microsoft.EntityFrameworkCore;
using tallerM.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<tallerM.Api.DataContext>(x => x.UseSqlServer("name=con"));
        builder.Services.AddTransient<Seeder>();

        var app = builder.Build();
        SeedApp(app);

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

    private static void SeedApp(WebApplication app)
    {
        IServiceScopeFactory? serviceScopeFactory =
            app.Services.GetService<IServiceScopeFactory>();
        using (IServiceScope? serviceScope =
            serviceScopeFactory!.CreateScope())
        { 
            Seeder? seeder =
                serviceScope.ServiceProvider.GetService<Seeder>();
            seeder!.SeedAsync().Wait();
        }
    }
}