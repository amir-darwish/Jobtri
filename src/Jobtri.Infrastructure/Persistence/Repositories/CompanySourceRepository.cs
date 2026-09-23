using Jobtri.Application.Abstractions.Persistence;
using Jobtri.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace Jobtri.Infrastructure.Persistence.Repositories;

public sealed class CompanySourceRepository : ICompanySourceRepository
{
    private readonly JobtriDbContext _dbContext;

    public CompanySourceRepository(JobtriDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(CompanySource companySource, CancellationToken cancellationToken = default)
    {
        await _dbContext.CompanySources.AddAsync(companySource, cancellationToken);
    }

    public async Task<CompanySource?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.CompanySources.FindAsync(id, cancellationToken);
    }

    public async Task<List<CompanySource>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.CompanySources.ToListAsync(cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}