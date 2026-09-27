using Jobtri.Application.Abstractions.Persistence;
using Jobtri.Domain.Entities;

namespace Jobtri.Application.JobTargets;

public sealed class JobTargetService
{
    private readonly IJobTargetRepository _jobTargetRepository;

    public JobTargetService(IJobTargetRepository jobTargetRepository)
    {
        _jobTargetRepository = jobTargetRepository;
    }

    public async Task<int> CreateAsync(
        string name,
        string role,
        string? country = null,
        string? domain = null,
        CancellationToken cancellationToken = default)
    {
        var jobTarget = new JobTarget(name, role, country, domain);

        await _jobTargetRepository.AddAsync(jobTarget, cancellationToken);
        await _jobTargetRepository.SaveChangesAsync(cancellationToken);

        return jobTarget.Id;
    }

    public async Task<JobTarget?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return await _jobTargetRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task<List<JobTarget>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _jobTargetRepository.GetAllAsync(cancellationToken);
    }

    public async Task<bool> EnableAsync(int id, CancellationToken cancellationToken = default)
    {
        var target = await _jobTargetRepository.GetByIdAsync(id, cancellationToken);

        if (target == null)
        {
            return false;
        }

        target.Enable();

        await _jobTargetRepository.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> DisableAsync(int id, CancellationToken cancellationToken = default)
    {
        var target = await _jobTargetRepository.GetByIdAsync(id, cancellationToken);

        if (target == null)
        {
            return false;
        }

        target.Disable();

        await _jobTargetRepository.SaveChangesAsync(cancellationToken);

        return true;
    }
}