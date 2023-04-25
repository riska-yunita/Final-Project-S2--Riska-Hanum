using API.Models;
using API.Repository.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountRolesController : BaseController<AccountRole, IAccountRolesRepository, int>
{
    private readonly IAccountRolesRepository repository;
    public AccountRolesController(IAccountRolesRepository _repository):base(_repository)
    {
        repository = _repository;
    }
}
