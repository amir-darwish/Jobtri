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
}