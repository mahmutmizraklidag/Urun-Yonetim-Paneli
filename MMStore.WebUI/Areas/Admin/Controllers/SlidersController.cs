using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MMStore.Entities;
using MMStore.Service.Repositories;
using MMStore.WebUI.Utils;

namespace MMStore.WebUI.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class SlidersController : Controller
    {
        private readonly IRepository<Slider> _repository;

        public SlidersController(IRepository<Slider> repository)
        {
            _repository = repository;
        }

        // GET: SlidersController
        public async Task<ActionResult> IndexAsync()
        {
            var model = await _repository.GetAllAsync();
            return View(model);
        }

        // GET: SlidersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SlidersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SlidersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Slider entity, IFormFile? Image)
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

        // GET: SlidersController/Edit/5
        public async Task<ActionResult> EditAsync(int? id)
        {
            if (id == null) { return BadRequest(); }
            var slider = await _repository.FindAsync(id.Value);
            if (slider == null) { return BadRequest(); }
            return View(slider);
        }

        // POST: SlidersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, Slider entity, IFormFile? Image, bool? resmiSil)
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

        // GET: SlidersController/Delete/5
        public async Task<ActionResult> DeleteAsync(int? id)
        {
            if (id == null) { return BadRequest(); }
            var slider = await _repository.FindAsync(id.Value);
            if (slider == null) { return BadRequest(); }
            return View(slider);
        }

        // POST: SlidersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(int id, Slider entity)
        {
            try
            {

                FileHelper.FileRemover(entity.Image);
                _repository.Delete(entity);
                await _repository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Hata Oluştu!");
            }

            return View(entity);
        }
    }
}