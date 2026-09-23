using Jobtri.Application.Abstractions.Persistence;
using Jobtri.Domain.Entities;

using Microsoft.EntityFrameworkCore;

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

        public async Task<Company?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Companies.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task<List<Company>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Companies.ToListAsync(cancellationToken);
        }
    }
}