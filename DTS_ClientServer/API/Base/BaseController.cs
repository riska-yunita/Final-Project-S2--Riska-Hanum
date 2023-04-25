using Microsoft.AspNetCore.Mvc;
using API.Repository.Contracts;
using System.Net;
using Microsoft.EntityFrameworkCore;
using API.Models;
using Microsoft.AspNetCore.Http;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace API.Controllers
{
    //protocol-domain-path/api/controller
    [Route("api/[controller]")]
    [ApiController]
    
    public class BaseController<TEntity, TRepository, TKey> : Controller
        where TEntity : class
        where TRepository : IGeneralRepository<TEntity, TKey>

    {
        private readonly TRepository repository;

        public BaseController(TRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public virtual async Task<ActionResult> Get()
        {
            try{
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
                return Ok(new {
                    code = StatusCodes.Status200OK,
                    status = HttpStatusCode.OK.ToString(),
                    data = results
                });
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
            
        }

        [HttpGet("{key}")]
        public virtual async Task<ActionResult> GetById(TKey key)
        {
            try{
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
                return Ok(new {
                    code = StatusCodes.Status200OK,
                    status = HttpStatusCode.OK.ToString(),
                    data = results
                });
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
            
        }

        [HttpPost]
        public virtual async Task<ActionResult<TEntity?>> Insert(TEntity entity)
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
                    message = "Insert Sukses", results
                }
            });
        }

        [HttpPut("{key}")]
        public virtual async Task<IActionResult> Update (TEntity entity, TKey key)
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
            if (update ==0)
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
        public virtual async Task<IActionResult> Delete(TKey key)
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
}
