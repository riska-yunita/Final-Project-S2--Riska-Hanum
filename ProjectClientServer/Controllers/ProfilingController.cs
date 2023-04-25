using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectClientServer.Models;
using ProjectClientServer.Repositories;
using ProjectClientServer.Repositories.Contract;
using ProjectClientServer.Repositories.Data;
using System.Net;

namespace ProjectClientServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilingController : ControllerBase
    {
        private readonly IProfilingRepository _profilingRepository;

        public ProfilingController(IProfilingRepository profilingRepository)
        {
            _profilingRepository = profilingRepository;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var results = await _profilingRepository.GetAllAsync();
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
            var results = await _profilingRepository.GetByIdAsync(nik);
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
        public async Task<ActionResult<Profiling?>> Insert(Profiling profiling)
        {
            var results = await _profilingRepository.InsertAsync(profiling);
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
        public async Task<ActionResult> Update(Profiling profiling)
        {
            var results = await _profilingRepository.UpdateAsync(profiling);
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
            var results = await _profilingRepository.DeleteAsync(nik);
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

        [HttpGet("AvgGPA/{tahun}")]
        public async Task<ActionResult> GetAvgGpa(int tahun)
        {
            
            var get = await _profilingRepository.GetAvgGpa(tahun);
            return Ok(new
            {
                code = StatusCodes.Status200OK,
                status = HttpStatusCode.OK.ToString(),
                data = get
            });
                
                //get == null ? NotFound(new { message = "Data not found" }) : Ok(_profilingRepository.GetAvgGpa(tahun));
            
        }

        [HttpGet("TotalByMajor")]
        public async Task<ActionResult> GetTotalByMajor()
        {
            var get = await _profilingRepository.TotalByMajor();
            return Ok(new
            {
                code = StatusCodes.Status200OK,
                status = HttpStatusCode.OK.ToString(),
                data = get
            });
        }

        [HttpGet("WorkPeriod")]
        public async Task<ActionResult> GetWorkPeriod()
        {
                var get = await _profilingRepository.WorkPeriod();
                return Ok(new
                {
                    code = StatusCodes.Status200OK,
                    status = HttpStatusCode.OK.ToString(),
                    data = get
                });
        }
    }
}
