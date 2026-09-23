using Jobtri.Application.Abstractions.Persistence;
using Jobtri.Domain.Entities;

namespace Jobtri.Infrastructure.Persistence.Repositories
{
    public sealed class CompanyRepository : ICompanyRepository
    {
        private readonly JobtriDbContext _dbContext;

        public CompanyRepository(JobtriDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Company company, CancellationToken cancellationToken = default)
        {
            await _dbContext.Companies.AddAsync(company, cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}