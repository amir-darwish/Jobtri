using System;
using System.Collections.Generic;
using System.Text;

using Jobtri.Domain.Entities;

namespace Jobtri.Application.Abstractions.Persistence;

public interface ICompanySourceRepository
{
    Task AddAsync(CompanySource companySource, CancellationToken cancellationToken = default);

    Task<CompanySource?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<List<CompanySource>> GetAllAsync(CancellationToken cancellationToken = default);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
