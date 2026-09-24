using Microsoft.AspNetCore.Mvc;

using Jobtri.Application.Jobs;

namespace Jobtri.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class JobsController : ControllerBase
{
    private readonly JobService _jobService;
    
    public JobsController(JobService jobService)
    {
        _jobService = jobService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateJob(string title, int companySourceId, string sourceJobId,Uri jobUrl,
        string? location = null, string? description = null, DateTimeOffset? datePosted = null, CancellationToken cancellationToken = default)
    {
        var id = await _jobService.CreateAsync(title, companySourceId, sourceJobId, jobUrl, location, description, datePosted, cancellationToken);

        return CreatedAtAction(nameof(GetJobById), new { id }, id);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetJobById(int id, CancellationToken cancellationToken = default)
    {
        var job = await _jobService.GetByIdAsync(id, cancellationToken);
        if (job == null)
        {
            return NotFound();
        }

        return Ok(job);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllJobs(CancellationToken cancellationToken = default)
    {
        var jobs = await _jobService.GetAllAsync(cancellationToken);
        return Ok(jobs);
    }

}
