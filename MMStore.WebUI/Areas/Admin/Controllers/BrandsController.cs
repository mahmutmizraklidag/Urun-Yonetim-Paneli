using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MMStore.Entities;
using MMStore.Service.Repositories;
using MMStore.WebUI.Utils;

namespace MMStore.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandsController : Controller
    {
        private readonly IRepository<Brand> _repository;

        public BrandsController(IRepository<Brand> repository)
        {
            _repository = repository;
        }

        // GET: BrandsController
        public async Task<ActionResult> Index()
        {
            var model = await _repository.GetAllAsync();
            return View(model);
        }

        // GET: BrandsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BrandsController/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: BrandsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Brand entity,IFormFile? Image)
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
            return View(entity);
        }

        // GET: BrandsController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            var brand = await _repository.FindAsync(id);
            return View(brand);
        }

        // POST: BrandsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, Brand entity,IFormFile? Image)
        {
            if (ModelState.IsValid)
            {
                try
                {
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

        // GET: BrandsController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var brand = await _repository.FindAsync(id);
            return View(brand);
        }

        // POST: BrandsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(int id,Brand entity)
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
