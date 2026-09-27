using Jobtri.Domain.Entities;

namespace Jobtri.Application.Abstractions.Persistence;

public interface IJobTargetRepository
{
    Task AddAsync(JobTarget jobTarget, CancellationToken cancellationToken = default);

    Task<JobTarget?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<List<JobTarget>> GetAllAsync(CancellationToken cancellationToken = default);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
     
}