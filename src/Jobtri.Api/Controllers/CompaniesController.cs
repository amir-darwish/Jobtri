using Jobtri.Application.Companies;

using Microsoft.AspNetCore.Mvc;

namespace Jobtri.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class CompaniesController : ControllerBase
{
    private readonly CompanyService _companyService;

    public CompaniesController(CompanyService companyService)
    {
        _companyService = companyService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(string name, CancellationToken cancellationToken)
    {
        var id = await _companyService.CreateAsync(name, cancellationToken);

        return Ok(new
        {
            Id = id
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var company = await _companyService.GetByIdAsync(id, cancellationToken);

        if (company == null)
        {
            return NotFound();
        }

        return Ok(company);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var companies = await _companyService.GetAllAsync(cancellationToken);

        return Ok(companies);
    }
}