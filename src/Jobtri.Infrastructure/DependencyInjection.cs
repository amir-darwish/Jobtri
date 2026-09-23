using Jobtri.Application.Abstractions.Persistence;
using Jobtri.Infrastructure.Persistence.Repositories;

using Microsoft.Extensions.DependencyInjection;

namespace Jobtri.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<ICompanyRepository, CompanyRepository>();

            return services;
        }
    }
}