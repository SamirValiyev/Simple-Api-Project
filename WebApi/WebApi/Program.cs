
using Application;
using Application.Features.CQRS.Queries;
using Infrastructure;
using Infrastructure.Tools;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Persistence;
using System.Reflection;
using System.Text;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            JwtDefaults.Initialize(builder.Configuration);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
                opt =>
                {
                    opt.RequireHttpsMetadata = false;
                    opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                       ValidateAudience = true,
                       ValidateIssuer = true,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                        ValidIssuer = JwtDefaults.ValidIssuer,
                        ValidAudience = JwtDefaults.ValidAudience,
                        IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtDefaults.SecretKey)),
                        ClockSkew=TimeSpan.Zero
                    };

                });

            // Add services to the container.
            RegistrationPersistence.ConfigureServices(builder.Services, builder.Configuration);
            RegistrationApplication.ConfigureServices(builder.Services, builder.Configuration);
            RegistrationInfrastructure.ConfigureServices(builder.Services, builder.Configuration);
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllProductQueryRequest).Assembly));


            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
