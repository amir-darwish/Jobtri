using Jobtri.Domain.Entities;


namespace Jobtri.Application.Abstractions.Persistence;

public interface IJobRepository
{
    public Task AddAsync(Job job, CancellationToken cancellationToken = default);
    public Task<Job?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    public Task<List<Job>> GetAllAsync(CancellationToken cancellationToken = default);
    public Task SaveChangesAsync(CancellationToken cancellationToken = default);

}
