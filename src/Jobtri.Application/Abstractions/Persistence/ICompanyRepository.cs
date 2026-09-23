using Jobtri.Domain.Entities;

namespace Jobtri.Application.Abstractions.Persistence
{
    public interface ICompanyRepository
    {
        Task AddAsync(Company company, CancellationToken cancellationToken = default);

        Task SaveChangesAsync(CancellationToken cancellationToken = default);
        
        Task<Company?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<List<Company>> GetAllAsync(CancellationToken cancellationToken = default);

    }
}