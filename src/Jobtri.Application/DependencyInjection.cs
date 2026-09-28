using Microsoft.Extensions.DependencyInjection;

namespace Jobtri.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<Companies.CompanyService>();
            services.AddScoped<CompanySources.CompanySourceService>();
            services.AddScoped<Jobs.JobService>();
            services.AddScoped<JobTargets.JobTargetService>();
            services.AddScoped<SavedJobs.SavedJobService>();
            return services;
        }
    }
}