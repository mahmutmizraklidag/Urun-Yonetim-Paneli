using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MMStore.Entities;
using MMStore.Service.Repositories;
using MMStore.WebUI.Utils;

namespace MMStore.WebUI.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class NewsController : Controller
    {
        private readonly IRepository<News> _repository;

        public NewsController(IRepository<News> repository)
        {
            _repository = repository;
        }

        // GET: NewsController
        public async Task<ActionResult> Index()
        {
            var news = await _repository.GetAllAsync();
            return View(news);
        }

        // GET: NewsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: NewsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NewsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(News entitiy, IFormFile? Image)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (Image is not null) entitiy.Image = await FileHelper.FileLoaderAsync(Image);
                    await _repository.AddAsync(entitiy);
                    await _repository.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }

            return View(entitiy);
        }

        // GET: NewsController/Edit/5
        public async Task<ActionResult> EditAsync(int? id)
        {
            if (id == null) { return BadRequest(); }
            var news = await _repository.FindAsync(id.Value);
            if (news == null) { return BadRequest(); }
            return View(news);
        }

        // POST: NewsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, News entitiy, IFormFile? Image, bool? resmiSil)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (resmiSil == true)
                    {
                        entitiy.Image = string.Empty;
                    }
                    if (Image is not null) entitiy.Image = await FileHelper.FileLoaderAsync(Image);
                    _repository.Update(entitiy);
                    await _repository.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }

            return View(entitiy);
        }

        // GET: NewsController/Delete/5
        public async Task<ActionResult> DeleteAsync(int? id)
        {
            if (id == null) { return BadRequest(); }
            var news = await _repository.FindAsync(id.Value);
            if (news == null) { return BadRequest();}
            return View(news);
        }

        // POST: NewsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(int id, News entity)
        {
            try
            {
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
