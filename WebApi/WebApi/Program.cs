
using Application;
using Application.Features.CQRS.Queries;
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

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
                opt =>
                {
                    opt.RequireHttpsMetadata = false;
                    opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidAudience = "http://localhost",
                        ValidIssuer = "http://localhost",
                        ClockSkew = TimeSpan.Zero,
                        IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes("samirsamirsamirsamirsamir")),
                        ValidateIssuerSigningKey=true,
                        ValidateLifetime = true
                    };

                });

            // Add services to the container.
            Registration.ConfigureServices(builder.Services, builder.Configuration);
            RegistrationApplication.ConfigureServicesApp(builder.Services, builder.Configuration);
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

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
