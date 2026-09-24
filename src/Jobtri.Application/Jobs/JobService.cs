using Jobtri.Application.Abstractions.Persistence;
using Jobtri.Domain.Entities;

namespace Jobtri.Application.Jobs;

public sealed class JobService
{
    private readonly IJobRepository _jobRepository;

    public JobService(IJobRepository jobRepository)
    {
        _jobRepository = jobRepository;
    }


    public async Task<int> CreateAsync(
        string title,
        int companySourceId,
        string sourceJobId,
        Uri jobUrl,
        string? location = null,
        string? description = null,
        DateTimeOffset? datePosted = null,
        CancellationToken cancellationToken = default)
    {
        var job = new Job(
            title,
            companySourceId,
            sourceJobId,
            jobUrl,
            location,
            description,
            datePosted);

        await _jobRepository.AddAsync(job, cancellationToken);
        await _jobRepository.SaveChangesAsync(cancellationToken);

        return job.Id;
    }


    public async Task<Job?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return await _jobRepository.GetByIdAsync(id, cancellationToken);
    }


    public async Task<List<Job>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _jobRepository.GetAllAsync(cancellationToken);
    }
}