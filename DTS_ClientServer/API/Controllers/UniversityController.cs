using API.Models;
using API.Repository.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "User")]
public class UniversityController : BaseController<University, IUniversityRepository, int>
{
    private readonly IUniversityRepository repository;
    public UniversityController(IUniversityRepository _repository) : base(_repository)
    {
        repository = _repository;
    }
}