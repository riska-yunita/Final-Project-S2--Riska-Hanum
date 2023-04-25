using API.Context;
using API.Controllers;
using API.Models;
using API.Repository.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProfilingsController : BaseController<Profiling, IProfilingsRepository, string>
{
    private readonly IProfilingsRepository repository;
    public ProfilingsController (IProfilingsRepository _repository) : base(_repository)
    {
        repository = _repository;
    }

    // GET
    [HttpGet("WorkPeriod")]
    public async Task<IActionResult> GetWorkPeriod()
    {
        var results = await repository.GetWorkPeriod();
        return Ok(new
        {
            code = StatusCodes.Status200OK,
            status = HttpStatusCode.OK.ToString(),
            data = results
        });
    }

    // GET
    [HttpGet("TotalByMajor")]
    public async Task<IActionResult> GetTotalByMajor()
    {
        var results = await repository.GetTotalByMajor();
        return Ok(new
        {
            code = StatusCodes.Status200OK,
            status = HttpStatusCode.OK.ToString(),
            data = results
        });
    }

    // GET
    [HttpGet("AvgGPA")]
    public async Task<IActionResult> GetAvgGPA(int year)
    {
        var results = await repository.GetAvgGPA(year);
        return Ok(new
        {
            code = StatusCodes.Status200OK,
            status = HttpStatusCode.OK.ToString(),
            data = results
        });
    }
}