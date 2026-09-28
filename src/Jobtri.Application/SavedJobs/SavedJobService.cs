using Jobtri.Application.Abstractions.Persistence;
using Jobtri.Domain.Entities;

namespace Jobtri.Application.SavedJobs;

public sealed class SavedJobService
{
    private readonly ISavedJobRepository _savedJobRepository;

    public SavedJobService(ISavedJobRepository savedJobRepository)
    {
        _savedJobRepository = savedJobRepository;
    }

    public async Task<int> CreateAsync(
        int jobId,
        string? notes = null,
        CancellationToken cancellationToken = default)
    {
        var savedJob = new SavedJob(jobId, notes);

        await _savedJobRepository.AddAsync(savedJob, cancellationToken);
        await _savedJobRepository.SaveChangesAsync(cancellationToken);

        return savedJob.Id;
    }

    public async Task<SavedJob?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return await _savedJobRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task<List<SavedJob>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _savedJobRepository.GetAllAsync(cancellationToken);
    }

    public async Task<List<SavedJob>> GetByJobIdAsync(
        int jobId,
        CancellationToken cancellationToken = default)
    {
        return await _savedJobRepository.GetByJobIdAsync(jobId, cancellationToken);
    }

    public async Task<bool> UpdateNotesAsync(
        int id,
        string? notes,
        CancellationToken cancellationToken = default)
    {
        var savedJob = await _savedJobRepository.GetByIdAsync(id, cancellationToken);

        if (savedJob == null)
        {
            return false;
        }

        savedJob.UpdateNotes(notes);

        await _savedJobRepository.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var savedJob = await _savedJobRepository.GetByIdAsync(id, cancellationToken);
        if (savedJob == null)
        {
            return false;
        }
        _savedJobRepository.Delete(savedJob);
        await _savedJobRepository.SaveChangesAsync(cancellationToken);
        return true;
    }
}
