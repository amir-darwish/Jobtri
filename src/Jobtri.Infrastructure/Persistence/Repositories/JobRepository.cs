using Jobtri.Domain.Entities;
using Jobtri.Application.Abstractions.Persistence;

using Microsoft.EntityFrameworkCore;

namespace Jobtri.Infrastructure.Persistence.Repositories;

public class JobRepository : IJobRepository
{
    private readonly JobtriDbContext _dbContext;
    public JobRepository(JobtriDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task AddAsync(Job job, CancellationToken cancellationToken = default)
    {
        await _dbContext.Jobs.AddAsync(job, cancellationToken);
    }
    public async Task<Job?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Jobs.FindAsync(id, cancellationToken);
    }
    public async Task<List<Job>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Jobs.ToListAsync(cancellationToken);
    }
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
