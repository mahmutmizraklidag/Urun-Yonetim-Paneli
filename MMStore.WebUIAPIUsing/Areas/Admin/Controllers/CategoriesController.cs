using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MMStore.Entities;
using MMStore.WebUIAPIUsing.Utils;

namespace MMStore.WebUIAPIUsing.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class CategoriesController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiAdress;
        public CategoriesController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apiAdress = "https://localhost:7231/api/Categories";
        }
        // GET: CategoriesController
        public async Task<ActionResult> IndexAsync()
        {
            var model = await _httpClient.GetFromJsonAsync<List<Category>>(_apiAdress);
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
            var list = await _httpClient.GetFromJsonAsync<List<Category>>(_apiAdress);
            ViewBag.ParentId = new SelectList(list, "Id", "Name");
            return View();
        }

        // POST: CategoriesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Category entity,IFormFile? Image)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (Image is not null) entity.Image = await FileHelper.FileLoaderAsync(Image);
                    var response = await _httpClient.PostAsJsonAsync(_apiAdress, entity);
                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index));
                    else ModelState.AddModelError("", "Kayıt Başarısız!");
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            var list = await _httpClient.GetFromJsonAsync<List<Category>>(_apiAdress);
            ViewBag.ParentId = new SelectList(list, "Id", "Name");
            return View(entity);
        }

        // GET: CategoriesController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            var user = await _httpClient.GetFromJsonAsync<Category>(_apiAdress + "/" + id);
            if (user == null) return NotFound();
            var list = await _httpClient.GetFromJsonAsync<List<Category>>(_apiAdress);
            ViewBag.ParentId = new SelectList(list, "Id", "Name");
            return View(user);
        }

        // POST: CategoriesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, Category entity, IFormFile? Image)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (Image is not null) entity.Image = await FileHelper.FileLoaderAsync(Image);
                    var response = await _httpClient.PutAsJsonAsync(_apiAdress + "/" + id, entity);
                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index));
                    else ModelState.AddModelError("", "Kayıt Başarısız!");
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            var list = await _httpClient.GetFromJsonAsync<List<Category>>(_apiAdress);
            ViewBag.ParentId = new SelectList(list, "Id", "Name");
            return View(entity);
        }

        // GET: CategoriesController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var user = await _httpClient.GetFromJsonAsync<Category>(_apiAdress + "/" + id);
            if (user == null) return NotFound();
            var list = await _httpClient.GetFromJsonAsync<List<Category>>(_apiAdress);
            ViewBag.ParentId = new SelectList(list, "Id", "Name");
            return View(user);
        }

        // POST: CategoriesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(int id, Category entity)
        {
            try
            {
                var user = await _httpClient.DeleteAsync(_apiAdress + "/" + id);
                if (user.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
                else ModelState.AddModelError("", "Kayıt Silinemedi!");
            }
            catch
            {
                ModelState.AddModelError("", "Hata Oluştu!");
            }
            return View(entity);
        }
    }
}
