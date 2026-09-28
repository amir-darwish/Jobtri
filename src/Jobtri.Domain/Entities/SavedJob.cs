namespace Jobtri.Domain.Entities;

public sealed class SavedJob
{
    public int Id { get; private set; }

    public int JobId { get; private set; }

    public DateTimeOffset SavedAt { get; private set; }

    public string? Notes { get; private set; }


    public SavedJob(int jobId, string? notes = null)
    {
        if (jobId <= 0)
        {
            throw new ArgumentException("Job ID must be positive.", nameof(jobId));
        }

        JobId = jobId;

        Notes = string.IsNullOrWhiteSpace(notes) ? null : notes.Trim();

        SavedAt = DateTimeOffset.UtcNow;
    }


    public void UpdateNotes(string? notes)
    {
        Notes = string.IsNullOrWhiteSpace(notes) ? null : notes.Trim();
    }
}