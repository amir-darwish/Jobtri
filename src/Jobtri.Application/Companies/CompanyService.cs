using Jobtri.Application.Abstractions.Persistence;
using Jobtri.Domain.Entities;

namespace Jobtri.Application.Companies
{
    public sealed class CompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<int> CreateAsync(string name, CancellationToken cancellationToken = default)
        {
            var company = new Company(name);

            await _companyRepository.AddAsync(company, cancellationToken);
            await _companyRepository.SaveChangesAsync(cancellationToken);

            return company.Id;
        }

        public async Task<Company?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _companyRepository.GetByIdAsync(id, cancellationToken);
        }

        public async Task<List<Company>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _companyRepository.GetAllAsync(cancellationToken);
        }

        public async Task<bool> EnableAsync(int id, CancellationToken cancellationToken = default)
        {
            var company = await _companyRepository.GetByIdAsync(id, cancellationToken);
            if (company == null)
            {
                return false;
            }
            company.Enable();
            await _companyRepository.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> DisableAsync(int id, CancellationToken cancellationToken = default)
        {
            var company = await _companyRepository.GetByIdAsync(id, cancellationToken);
            if (company == null)
            {
                return false;
            }
            company.Disable();
            await _companyRepository.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> UpdateWebsiteAsync(int id, string website, CancellationToken cancellationToken = default)
        {
            var company = await _companyRepository.GetByIdAsync(id, cancellationToken);
            if (company == null)
            {
                return false;
            }
            company.SetWebsite(website);
            await _companyRepository.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}