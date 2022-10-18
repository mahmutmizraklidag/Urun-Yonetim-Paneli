using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MMStore.Entities;
using MMStore.Service.Repositories;
using MMStore.WebUI.Utils;


namespace MMStore.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly IRepository<Category> _repository;

        public CategoriesController(IRepository<Category> repository)
        {
            _repository = repository;
        }

        // GET: CategoriesController
        public async Task<ActionResult> Index()
        {
            var model = await _repository.GetAllAsync();
            return View(model);
        }

        // GET: CategoriesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CategoriesController/Create
        public async Task<ActionResult> CreateAsync()
        {
            var list = await _repository.GetAllAsync();
            ViewBag.ParentId = new SelectList(list,"Id","Name");
            return View();
        }

        // POST: CategoriesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Category entity, IFormFile? Image)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (Image is not null) entity.Image = await FileHelper.FileLoaderAsync(Image);

                    await _repository.AddAsync(entity);
                    await _repository.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            var list = await _repository.GetAllAsync();
            ViewBag.ParentId = new SelectList(list, "Id", "Name");
            return View(entity);
        }

        // GET: CategoriesController/Edit/5
        public async Task<ActionResult> EditAsync(int? id)
        {
            if (id == null) { return BadRequest(); }
            var category = await _repository.FindAsync(id.Value);
            if (category == null) { return BadRequest(); }
            var list = await _repository.GetAllAsync();
            ViewBag.ParentId = new SelectList(list, "Id", "Name");
            return View(category);
        }

        // POST: CategoriesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, Category entity, IFormFile? Image,bool? resmiSil)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (resmiSil == true)
                    {
                        entity.Image = string.Empty;
                    }
                    if (Image is not null) entity.Image = await FileHelper.FileLoaderAsync(Image);

                    _repository.Update(entity);
                    await _repository.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            return View(entity);
        }

        // GET: CategoriesController/Delete/5
        public async Task<ActionResult> DeleteAsync(int? id)
        {
            if (id == null) { return BadRequest(); }
            var category = await _repository.FindAsync(id.Value);
            if (category == null) { return BadRequest(); }
            var list = await _repository.GetAllAsync();
            ViewBag.ParentId = new SelectList(list, "Id", "Name");
            return View(category);
        }

        // POST: CategoriesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(int id, Category entity)
        {
            try
            {
                _repository.Delete(entity);
                await _repository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(entity);
            }
        }
    }
}
