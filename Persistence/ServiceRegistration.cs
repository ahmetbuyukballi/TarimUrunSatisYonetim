using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistence(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer("Server=AHMET\\MSSQLSERVER_2022;Database=TarimDb;User Id=sa;Password=123456;TrustServerCertificate=True;"));
        }
       
    }
}
