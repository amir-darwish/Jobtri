using Jobtri.Application.Abstractions.Persistence;
using Jobtri.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace Jobtri.Infrastructure.Persistence.Repositories;

public class JobTargetRepository : IJobTargetRepository
{
    private readonly JobtriDbContext _dbContext;
    public JobTargetRepository(JobtriDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task AddAsync(JobTarget jobTarget, CancellationToken cancellationToken = default)
    {
        await _dbContext.JobTargets.AddAsync(jobTarget, cancellationToken);
    }
    public async Task<JobTarget?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.JobTargets.FindAsync(new object[] { id }, cancellationToken);
    }
    public async Task<List<JobTarget>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.JobTargets.ToListAsync(cancellationToken);
    }
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
