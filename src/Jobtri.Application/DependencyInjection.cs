using Microsoft.Extensions.DependencyInjection;

namespace Jobtri.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<Companies.CompanyService>();

            return services;
        }
    }
}