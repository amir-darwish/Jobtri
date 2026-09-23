using Jobtri.Application.Abstractions.Persistence;
using Jobtri.Domain.Entities;
using Jobtri.Domain.Enums;

namespace Jobtri.Application.CompanySources;

public class CompanySourceService
{
    private readonly ICompanySourceRepository _repository;

    public CompanySourceService(ICompanySourceRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> CreateAsync(int companyId, Uri careersUrl, enAtsType ats,
        string? atsIdentifier = null, CancellationToken cancellationToken = default)
    {
        var companySource = new CompanySource(companyId, careersUrl, ats, atsIdentifier);

        await _repository.AddAsync(companySource, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return companySource.Id;
    }
    public async Task<CompanySource?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _repository.GetByIdAsync(id, cancellationToken);
    }

    public async Task<List<CompanySource>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _repository.GetAllAsync(cancellationToken);
    }

    public async Task EnableCompanySourceAsync(int id, CancellationToken cancellationToken = default)
    {
        var companySource = await _repository.GetByIdAsync(id, cancellationToken);
        if (companySource == null)
        {
            throw new InvalidOperationException($"Company source with ID {id} not found.");
        }
        companySource.Enable();
        await _repository.SaveChangesAsync(cancellationToken);
    }

    public async Task DisableCompanySourceAsync(int id, CancellationToken cancellationToken = default)
    {
        var companySource = await _repository.GetByIdAsync(id, cancellationToken);
        if (companySource == null)
        {
            throw new InvalidOperationException($"Company source with ID {id} not found.");
        }
        companySource.Disable();
        await _repository.SaveChangesAsync(cancellationToken);
    }

}
