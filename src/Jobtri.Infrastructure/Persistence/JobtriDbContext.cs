using Jobtri.Domain.Entities;
using Jobtri.Infrastructure.Persistence.Configurations;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CompanyConfiguration());
            modelBuilder.ApplyConfiguration(new CompanySourceConfiguration());
            modelBuilder.ApplyConfiguration(new JobConfiguration());
        }

    }
}