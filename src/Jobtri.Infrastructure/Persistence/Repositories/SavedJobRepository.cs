using Jobtri.Application.Abstractions.Persistence;
using Jobtri.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace Jobtri.Infrastructure.Persistence.Repositories;

public sealed class SavedJobRepository : ISavedJobRepository
{
    private readonly JobtriDbContext _dbContext;

    public SavedJobRepository(JobtriDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(SavedJob savedJob, CancellationToken cancellationToken = default)
    {
        await _dbContext.SavedJobs.AddAsync(savedJob, cancellationToken);
    }

    public async Task<SavedJob?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.SavedJobs.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task<List<SavedJob>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SavedJobs.ToListAsync(cancellationToken);
    }

    public async Task<List<SavedJob>> GetByJobIdAsync(int jobId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.SavedJobs
            .Where(s => s.JobId == jobId)
            .ToListAsync(cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(SavedJob savedJob, CancellationToken cancellationToken = default)
    {
        _dbContext.SavedJobs.Remove(savedJob);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
