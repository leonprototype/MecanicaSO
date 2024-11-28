using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using tallerM.Api;
using tallerM.Api.Helpers;
using tallerM.Shared.Entities;

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
        builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer("name=con"));
        builder.Services.AddScoped<IUserHelper, UserHelper>();
        builder.Services.AddTransient<Seeder>();

        builder.Services.AddIdentity<User, IdentityRole>(
            x =>
            {
                x.User.RequireUniqueEmail = true;
                x.Password.RequireDigit = false;
                x.Password.RequireNonAlphanumeric = false;
                x.Password.RequireLowercase = false;
                x.Password.RequireUppercase = false;
                x.Password.RequiredUniqueChars = 0;
                x.Password.RequiredLength = 6;

            }).AddEntityFrameworkStores<DataContext>()
            .AddDefaultTokenProviders();
        builder.Services.AddAuthentication(
            JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(x => x.TokenValidationParameters = new
            TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes
                (builder.Configuration["jwtKey"]!)),
                ClockSkew = TimeSpan.Zero,

            });
        var app = builder.Build();
        SeedApp(app);

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();

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