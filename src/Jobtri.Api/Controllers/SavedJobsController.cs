using Jobtri.Application.SavedJobs;

using Microsoft.AspNetCore.Mvc;


namespace Jobtri.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class SavedJobsController : ControllerBase
{
    private readonly SavedJobService _savedJobService;
    public SavedJobsController(SavedJobService savedJobService)
    {
        _savedJobService = savedJobService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateSavedJob(
        int jobId,
        string? notes = null,
        CancellationToken cancellationToken = default)
    {
        var id = await _savedJobService.CreateAsync(jobId, notes, cancellationToken);
        return CreatedAtAction(nameof(GetSavedJobById), new { id }, id);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSavedJobById(
        int id,
        CancellationToken cancellationToken = default)
    {
        var savedJob = await _savedJobService.GetByIdAsync(id, cancellationToken);
        if (savedJob == null)
        {
            return NotFound();
        }
        return Ok(savedJob);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSavedJobs(
        CancellationToken cancellationToken = default)
    {
        var savedJobs = await _savedJobService.GetAllAsync(cancellationToken);
        return Ok(savedJobs);
    }

    [HttpGet("by-job/{jobId}")]
    public async Task<IActionResult> GetSavedJobsByJobId(
        int jobId,
        CancellationToken cancellationToken = default)
    {
        var savedJobs = await _savedJobService.GetByJobIdAsync(jobId, cancellationToken);
        return Ok(savedJobs);
    }

    [HttpPut("{id}/notes")]
    public async Task<IActionResult> UpdateSavedJobNotes(
        int id,
        string? notes,
        CancellationToken cancellationToken = default)
    {
        var updated = await _savedJobService.UpdateNotesAsync(id, notes, cancellationToken);
        if (!updated)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSavedJob(
        int id,
        CancellationToken cancellationToken = default)
    {
        var deleted = await _savedJobService.DeleteAsync(id, cancellationToken);
        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }
}
