using Jobtri.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace Jobtri.Infrastructure.Persistence
{
    public sealed class JobtriDbContext : DbContext
    {
        public DbSet<Company> Companies
        {
            get
            {
                return Set<Company>();
            }
        }

        public DbSet<CompanySource> CompanySources
        {
            get
            {
                return Set<CompanySource>();
            }
        }

        public DbSet<Job> Jobs
        {
            get
            {
                return Set<Job>();
            }
        }

        public JobtriDbContext(DbContextOptions<JobtriDbContext> options) : base(options)
        {
        }
    }
}