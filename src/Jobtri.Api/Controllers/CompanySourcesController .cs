using Jobtri.Application.CompanySources;
using Jobtri.Domain.Enums;

using Microsoft.AspNetCore.Mvc;


namespace Jobtri.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public sealed class CompanySourcesController : ControllerBase
{
    private readonly CompanySourceService _companySourceService;

    public CompanySourcesController(CompanySourceService companySourceService)
    {
        _companySourceService = companySourceService;
    }
    [HttpPost]
    public async Task<IActionResult> CreateCompanySource(int companyId, Uri careersUrl, enAtsType ats,
        string? atsIdentifier = null, CancellationToken cancellationToken = default)
    {
        var id = await _companySourceService.CreateAsync(companyId, careersUrl, ats, atsIdentifier, cancellationToken);
        return CreatedAtAction(nameof(GetCompanySource), new { id }, id);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCompanySource(int id, CancellationToken cancellationToken = default)
    {
        var companySource = await _companySourceService.GetByIdAsync(id);
        if (companySource == null)
        {
            return NotFound();
        }
        return Ok(companySource);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCompanySources(CancellationToken cancellationToken)
    {
        var companySources = await _companySourceService.GetAllAsync(cancellationToken);
        return Ok(companySources);
    }

    [HttpPut("{id}/enable")]
    public async Task<IActionResult> EnableCompanySource(int id, CancellationToken cancellationToken)
    {
        try
        {
            await _companySourceService.EnableCompanySourceAsync(id, cancellationToken);
            return NoContent();
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

    [HttpPut("{id}/disable")]
    public async Task<IActionResult> DisableCompanySource(int id, CancellationToken cancellationToken)
    {
        try
        {
            await _companySourceService.DisableCompanySourceAsync(id, cancellationToken);
            return NoContent();
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

}
