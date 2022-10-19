using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MMStore.Entities;
using MMStore.Service.Repositories;

namespace MMStore.WebUI.Areas.Admin.Controllers
{
    [Area("Admin"),Authorize]
    public class AppUsersController : Controller
    {
        private readonly IRepository<AppUser> _repository;

        public AppUsersController(IRepository<AppUser> repository)
        {
            _repository = repository;
        }

        // GET: AppUsersController
        public async Task<ActionResult> Index()
        {
            var model = await _repository.GetAllAsync();
            return View(model);
        }

        // GET: AppUsersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AppUsersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AppUsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(AppUser entity)
        {
            if (ModelState.IsValid)
            {
                try
                {
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

        // GET: AppUsersController/Edit/5
        public async Task<ActionResult> EditAsync(int? id)
        {
            if (id == null) { return BadRequest(); }
            var user = await _repository.FindAsync(id.Value);
            if (user == null) { return BadRequest(); }
            return View(user);
        }

        // POST: AppUsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, AppUser appUser)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _repository.Update(appUser);
                    await _repository.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            
            return View(appUser);
        }

        // GET: AppUsersController/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null) { return BadRequest(); }
            var user = await _repository.FindAsync(id.Value);
            if (user == null) { return BadRequest(); }
            return View(user);
        }

        // POST: AppUsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, AppUser appUser)
        {
            try
            {
                _repository.Delete(appUser);
                await _repository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                
            }
            return View(appUser);
        }
    }
}
