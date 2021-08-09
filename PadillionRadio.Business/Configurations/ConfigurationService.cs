using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PadillionRadio.Data.Contexts;

namespace PadillionRadio.Business.Configurations
{
    public static class ConfigService
    {
        public static IServiceCollection InitializeServices(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddDbContextPool<DatabaseContext>
                (options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        }
    }
}
