using Microsoft.AspNetCore.Mvc;
using ProjectClientServer.Repositories.Contract;

namespace ProjectClientServer.Controllers
{
    public class GeneralController<TEntity, TRepository, TKey> : Controller
        where TEntity : class
        where TRepository : IGeneralRepository<TEntity, TKey>
    {
        private readonly TRepository repository;

        public GeneralController(TRepository repository)
        {
            this.repository = repository;
        }

        // GET: api/General
        /*[HttpGet]
        public async Task<ActionResult<IEnumerable<TEntity>>> Index()
        {
            return View(await repository.GetAll());
        }

        [HttpGet("Search")]
        public async Task<ActionResult<TEntity>> GetBy(TKey id)
        {
            var entity = await repository.GetById(id);
            return View(entity);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Insert")]
        [ValidateAntiForgeryToken]
        public IActionResult Insert(TEntity entity)
        {
            repository.Insert(entity);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Edit")]
        public async Task<IActionResult> Edit(TKey id)
        {
            var entities = await repository.GetById(id);
            return View(entities);
        }

        [HttpPost("Update")]
        [ValidateAntiForgeryToken]
        public IActionResult Update(TEntity entity)
        {
            repository.Update(entity);
            return RedirectToAction("Index");
        }

        //DELETE
        [HttpGet("Delete")]
        public async Task<IActionResult> Delete(TKey id)
        {
            var entities = await repository.GetById(id);
            return View(entities);
        }

        [HttpPost("Remove")]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(TKey id)
        {
            repository.Delete(id);
            return RedirectToAction("Index");
        }*/
    }
}
