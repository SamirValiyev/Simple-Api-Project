using Application.Mappers;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class RegistrationApplication
    {
        public static void ConfigureServicesApp(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(opt =>
            {
                opt.AddProfiles(new List<Profile>
                {
                        new ProductProfile()
                });
            });
            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        }
    }
}
