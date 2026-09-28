using Jobtri.Domain.Entities;

namespace Jobtri.Application.Abstractions.Persistence;

public interface ISavedJobRepository
{
    Task AddAsync(SavedJob savedJob, CancellationToken cancellationToken = default);

    Task<SavedJob?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<List<SavedJob>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<List<SavedJob>> GetByJobIdAsync(int jobId, CancellationToken cancellationToken = default);
    Task DeleteAsync(SavedJob savedJob, CancellationToken cancellationToken = default);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);

}
