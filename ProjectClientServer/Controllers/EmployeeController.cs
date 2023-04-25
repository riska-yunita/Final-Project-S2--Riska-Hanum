using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using NuGet.Protocol.Core.Types;
using ProjectClientServer.Models;
using ProjectClientServer.Repositories;
using ProjectClientServer.Repositories.Contract;
using ProjectClientServer.ViewModel;
using System.Collections;
using System.Net;

namespace ProjectClientServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository) //dependency injection
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var results = await _employeeRepository.GetAllAsync();
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
            var results = await _employeeRepository.GetByIdAsync(nik);
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
        public async Task<ActionResult<Employee?>> Insert(Employee employee)
        {
            var results = await _employeeRepository.InsertAsync(employee);
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
        public async Task<ActionResult> Update(Employee employee)
        {
            var results = await _employeeRepository.UpdateAsync(employee);
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

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var results = await _employeeRepository.DeleteAsync(id);
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

        [HttpGet("Master")]
        public async Task<ActionResult> GetMasterData()
        {
            try
            {
                var get = await _employeeRepository.MasterData();
                return get == null ? NotFound(new { message = "Data not found" }) : Ok(_employeeRepository.MasterData());
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}