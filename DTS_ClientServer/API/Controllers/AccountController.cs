using API.Handlers;
using API.Models;
using API.Repository.Contracts;
using API.Repository.Data;
using API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using System.Security.Claims;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles="Admin")]
public class AccountController : BaseController<Account, IAccountRepository, string>
{
    private readonly IAccountRepository repository;
    private readonly ITokenService tokenService;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IAccountRolesRepository _accountRolesRepository;

    public AccountController(IAccountRepository _repository,
        ITokenService _tokenService,
        IEmployeeRepository employeeRepository,
        IAccountRolesRepository accountRolesRepository): base(_repository)
    {
        repository = _repository;
        tokenService = _tokenService;
        _employeeRepository = employeeRepository;
        _accountRolesRepository = accountRolesRepository;   
    }

    [HttpPost("Auth")]
    [AllowAnonymous]
    public async Task<ActionResult> Login(LoginVM loginVM)
    {

        try
        {
            var result = await repository.LoginAsync(loginVM);
            if (!result)
            {
                return NotFound();
            }

            var userdata = await _employeeRepository.GetUserDataByEmailAsync(loginVM.Email);
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, userdata.Email),
                new Claim(ClaimTypes.Name, userdata.Email),
                new Claim(ClaimTypes.NameIdentifier, userdata.FullName),
                new Claim("NIK", userdata.Nik)
            };

            var getRoles = await _accountRolesRepository.GetRolesByNikAsync(userdata.Nik);

            foreach (var item in getRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, item));
            }

            var accessToken = tokenService.GenerateAccessToken(claims);
            var refreshToken = tokenService.GenerateRefreshToken();

            //await _repository.UpdateToken(userdata.Email, refreshToken, DateTime.Now.AddDays(1)); // Token will expired in a day

            return Ok(accessToken);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                              new
                              {
                                  Code = StatusCodes.Status500InternalServerError,
                                  Status = "Internal Server Error",
                                  Errors = new
                                  {
                                      Message = "Invalid Salt Version"
                                  },
                              });
        }
    }

    [HttpPost("Register")]
    [AllowAnonymous]
    public async Task<ActionResult> Register(RegisterVM registerVM)
    {
        try
        {
            await repository.RegisterAsync(registerVM);
            return Ok("Register Berhasil");
        }
        catch
        {
            return BadRequest("Register gagal");
        }
    }
    
}
