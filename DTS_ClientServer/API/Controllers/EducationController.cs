using API.Models;
using API.Repository.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Net;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EducationController : BaseController<Education, IEducationRepository, int>
{
    private readonly IEducationRepository repository;
    public EducationController(IEducationRepository _repository) : base(_repository)
    {
        repository = _repository;
    }

    [HttpGet]
    [Authorize(Roles = "User")]
    public override async Task<ActionResult> Get()
    {
        try
        {
            var results = await repository.GetAllAsync();
            if (results == null)
            {
                return NotFound(new
                {
                    code = StatusCodes.Status404NotFound,
                    status = HttpStatusCode.NotFound.ToString(),
                    data = new
                    {
                        message = "Data Tidak Ada"
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
        catch (Exception e)
        {
            return BadRequest(e);
        }

    }

    [HttpGet("{key}")]
    [Authorize(Roles = "User")]
    public override async Task<ActionResult> GetById(int key)
    {
        try
        {
            var results = await repository.GetByIdAsync(key);
            if (results == null)
            {
                return NotFound(new
                {
                    code = StatusCodes.Status404NotFound,
                    status = HttpStatusCode.NotFound.ToString(),
                    data = new
                    {
                        message = "Data Tidak Ada"
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
        catch (Exception e)
        {
            return BadRequest(e);
        }

    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public override async Task<ActionResult<Education?>> Insert(Education entity)
    {

        var results = await repository.InsertAsync(entity);
        if (results == null)
        {
            return Conflict(new
            {
                code = StatusCodes.Status409Conflict,
                status = HttpStatusCode.Conflict.ToString(),
                data = new
                {
                    message = "Data tidak berhasil disimpan"
                }
            });
        }
        return Ok(new
        {
            code = StatusCodes.Status200OK,
            status = HttpStatusCode.OK.ToString(),
            data = new
            {
                message = "Insert Sukses",
                results
            }
        });
    }

    [HttpPut("{key}")]
    [Authorize(Roles = "Admin")]
    public override async Task<IActionResult> Update(Education entity, int key)
    {
        var results = await repository.IsExist(key);
        if (!results)
        {
            return NotFound(new
            {
                code = StatusCodes.Status404NotFound,
                status = HttpStatusCode.NotFound.ToString(),
                data = new
                {
                    message = "Data Not Found"
                }
            });
        }
        var update = await repository.UpdateAsync(entity);
        if (update == 0)
        {
            return Conflict(new
            {
                code = StatusCodes.Status409Conflict,
                status = HttpStatusCode.Conflict.ToString(),
                data = new
                {
                    message = "Data tidak berhasil diupdate"
                }
            });
        }
        return Ok(new
        {
            code = StatusCodes.Status200OK,
            status = HttpStatusCode.OK.ToString(),
            data = new
            {
                message = "Data berhasil diupdate"
            }
        });
    }

    [HttpDelete("{key}")]
    [Authorize(Roles = "Admin")]
    public override async Task<IActionResult> Delete(int key)
    {
        await repository.DeleteAsync(key);

        return Ok(new
        {
            code = StatusCodes.Status200OK,
            status = HttpStatusCode.OK.ToString(),
            data = new
            {
                message = "Data berhasil dihapus"
            }
        });
    }
}