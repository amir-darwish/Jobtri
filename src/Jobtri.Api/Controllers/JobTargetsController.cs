using Jobtri.Application.JobTargets;

using Microsoft.AspNetCore.Mvc;

namespace Jobtri.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class JobTargetsController : ControllerBase
{
    private readonly JobTargetService _jobTargetService;

    public JobTargetsController(JobTargetService jobTargetService)
    {
        _jobTargetService = jobTargetService;
    }


    [HttpPost]
    public async Task<IActionResult> CreateJobTarget(
        string name,
        string role,
        string? country = null,
        string? domain = null,
        CancellationToken cancellationToken = default)
    {
        var id = await _jobTargetService.CreateAsync(
            name,
            role,
            country,
            domain,
            cancellationToken);

        return CreatedAtAction(
            nameof(GetJobTargetById),
            new { id },
            id);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetJobTargetById(
        int id,
        CancellationToken cancellationToken = default)
    {
        var target = await _jobTargetService.GetByIdAsync(id, cancellationToken);

        if (target == null)
        {
            return NotFound();
        }

        return Ok(target);
    }


    [HttpGet]
    public async Task<IActionResult> GetAllJobTargets(
        CancellationToken cancellationToken = default)
    {
        var targets = await _jobTargetService.GetAllAsync(cancellationToken);

        return Ok(targets);
    }


    [HttpPut("{id}/enable")]
    public async Task<IActionResult> Enable(
        int id,
        CancellationToken cancellationToken = default)
    {
        var result = await _jobTargetService.EnableAsync(id, cancellationToken);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }


    [HttpPut("{id}/disable")]
    public async Task<IActionResult> Disable(
        int id,
        CancellationToken cancellationToken = default)
    {
        var result = await _jobTargetService.DisableAsync(id, cancellationToken);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}