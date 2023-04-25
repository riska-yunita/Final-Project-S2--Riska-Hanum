using API.Models;
using API.Repository;
using API.Repository.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoleController : BaseController<Role, IRoleRepository, int>
{
    private readonly IRoleRepository repository;
    public RoleController(IRoleRepository _repository) : base(_repository)
    {
        repository = _repository;
    }
}