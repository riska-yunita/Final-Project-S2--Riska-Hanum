using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using NuGet.Protocol.Plugins;
using ProjectClientServer.Handler;
using ProjectClientServer.Models;
using ProjectClientServer.Repositories;
using ProjectClientServer.Repositories.Contract;
using ProjectClientServer.ViewModel;
using System.Net;
using System.Security.Claims;

namespace ProjectClientServer.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAccountRoleRepository _accountRoleRepository;
        private readonly ITokenService _tokenService;

        public AccountController(IAccountRepository accountRepository, IEmployeeRepository employeeRepository, IAccountRoleRepository accountRoleRepository, ITokenService tokenService)
        {
            _accountRepository = accountRepository;
            _employeeRepository = employeeRepository;
            _accountRoleRepository = accountRoleRepository;
            _tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpPost("Auth")]
        public async Task<ActionResult<Account>> Login(LoginVM loginVM)
        {
            try
            {
                var login = await _accountRepository.LoginAsync(loginVM);
                if (!login)
                {
                    return NotFound();
                }

                var user = await _employeeRepository.GetUserByEmail(loginVM.Email);
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.FullName),
                    new Claim("NIK", user.Nik)
                };

                var getRole = await _accountRoleRepository.GetRolesByNikAsync(user.Nik);
                
                foreach (var item in getRole)
                {
                    claims.Add(new Claim(ClaimTypes.Role, item));
                }

                var accessToken = _tokenService.GenerateAccessToken(claims);
                //return Ok(accessToken);

                var refreshToken = _tokenService.GenerateRefreshToken();
                //return Ok(refreshToken);

                var generatedToken = new
                {
                    Token = accessToken,
                    RefreshToken = refreshToken,
                    TokenType = "Bearer"
                };

                return Ok(generatedToken);
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

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<ActionResult<Account>> Register(RegisterVM registerVM)
        {
            try
            {
                await _accountRepository.RegisterAsync(registerVM);
                return Ok("registrasi success");
            }
            catch
            {
                return BadRequest("registrasi gagal");
            }

            //return register is null ? NotFound(new { status = HttpStatusCode.NotFound, message = "register gagal." }) 
            //  : Ok(new {status = HttpStatusCode.OK, message = "register berhasil." });

        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var results = await _accountRepository.GetAllAsync();
            if (results == null)
            {
                return NotFound(new
                {
                    code = StatusCodes.Status404NotFound,
                    status = HttpStatusCode.NotFound.ToString(),
                    data = new
                    {
                        message = "Data Not Found!"
                    }
                });
            }

            return Ok(new
            {
                code = StatusCodes.Status200OK,
                status = HttpStatusCode.OK.ToString(),
                data = results
            });
        }

        [HttpGet("{nik}")]
        public async Task<ActionResult> GetById(string nik)
        {
            var results = await _accountRepository.GetByIdAsync(nik);
            if (results == null)
            {
                return NotFound(new
                {
                    code = StatusCodes.Status404NotFound,
                    status = HttpStatusCode.NotFound.ToString(),
                    data = new
                    {
                        message = "Data Not Found!"
                    }
                });
            }

            return Ok(new
            {
                code = StatusCodes.Status200OK,
                status = HttpStatusCode.OK.ToString(),
                data = results
            });
        }

        [HttpPost]
        public async Task<ActionResult<Account?>> Insert(Account account)
        {
            var results = await _accountRepository.InsertAsync(account);
            if (results == null)
            {
                return Conflict(new
                {
                    code = StatusCodes.Status409Conflict,
                    status = HttpStatusCode.NotFound.ToString(),
                    data = new
                    {
                        message = "cannot insert data!"
                    }
                });
            }

            return Ok(new
            {
                code = StatusCodes.Status200OK,
                status = HttpStatusCode.OK.ToString(),
                data = new
                {
                    message = "Insert success",
                    results
                }
            });
        }

        [HttpPut]
        public async Task<ActionResult> Update(Account account)
        {
            var results = await _accountRepository.UpdateAsync(account);
            if (results == 0)
            {
                return NotFound(new
                {
                    code = StatusCodes.Status404NotFound,
                    status = HttpStatusCode.NotFound.ToString(),
                    data = new
                    {
                        message = "Data Not Found!"
                    }
                });
            }
            return Ok(new
            {
                code = StatusCodes.Status200OK,
                status = HttpStatusCode.OK.ToString(),
                data = new
                {
                    message = "Update success",
                    results
                }
            });
        }

        [HttpDelete("{nik}")]
        public async Task<ActionResult> Delete(string nik)
        {
            var results = await _accountRepository.DeleteAsync(nik);
            if (results == 0)
            {
                return NotFound(new
                {
                    code = StatusCodes.Status404NotFound,
                    status = HttpStatusCode.NotFound.ToString(),
                    data = new
                    {
                        message = "Data Not Found!"
                    }
                });
            }

            return Ok(new
            {
                code = StatusCodes.Status200OK,
                status = HttpStatusCode.OK.ToString(),
                data = new
                {
                    message = "Delete suscess"
                }
            });
        }

    }
}
