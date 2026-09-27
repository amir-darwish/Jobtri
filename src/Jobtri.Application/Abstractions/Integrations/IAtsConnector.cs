using Jobtri.Domain.Entities;
using Jobtri.Domain.Enums;

namespace Jobtri.Application.Abstractions.Integrations;

public interface IAtsConnector
{
    enAtsType Type { get; }

    Task<List<Job>> FetchJobsAsync(CompanySource companySource,CancellationToken cancellationToken = default);
}