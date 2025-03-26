using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class Registration
    {
        public static void ConfigureServices(IServiceCollection services,IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("LocalCon");
            services.AddDbContext<SimpleDbContext>(opt=>opt.UseSqlServer(connectionString));    

        }
    }
}
