using API.Models;
using API.Repository.Contracts;
using API.Repository.Data;
using API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Net;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : BaseController<Employee, IEmployeeRepository, string>
{
    private readonly IEmployeeRepository repository;
    public EmployeeController(IEmployeeRepository _repository) : base(_repository)
    {
        repository = _repository;
    }

    // GET
    [HttpGet("EmployeeMaster")]
    public async Task<IActionResult> GetEmployeeMaster()
    {
        var results = await repository.GetEmployeeMaster();
        return Ok(new
        {
            code = StatusCodes.Status200OK,
            status = HttpStatusCode.OK.ToString(),
            data = results
        });
    }
}
