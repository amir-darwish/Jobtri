using Jobtri.Domain.Entities;

namespace Jobtri.Application.Abstractions.Persistence
{
    public interface ICompanyRepository
    {
        Task AddAsync(Company company, CancellationToken cancellationToken = default);

        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}