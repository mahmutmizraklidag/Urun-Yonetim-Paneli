using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MMStore.Entities;
using MMStore.WebUIAPIUsing.Utils;

namespace MMStore.WebUIAPIUsing.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiAdress;
        private readonly string _apiAdressCategory;
        private readonly string _apiAdressBrand;
        public ProductsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apiAdress = "https://localhost:7231/api/Products";
            _apiAdressCategory = "https://localhost:7231/api/Categories";
            _apiAdressBrand = "https://localhost:7231/api/Brands";
        }
        // GET: ProductsController
        public async Task<ActionResult> Index()
        {
            var model = await _httpClient.GetFromJsonAsync<List<Product>>(_apiAdress);
            return View(model);
        }

        // GET: ProductsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductsController/Create
        public async Task<ActionResult> Create()
        {
            var list = await _httpClient.GetFromJsonAsync<List<Category>>(_apiAdressCategory);
            ViewBag.CategoryId = new SelectList(list, "Id", "Name");
            var brand = await _httpClient.GetFromJsonAsync<List<Brand>>(_apiAdressBrand);
            ViewBag.BrandId = new SelectList(brand, "Id", "Name");
            return View();
        }

        // POST: ProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Product entity, IFormFile? Image)
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
            var list = await _httpClient.GetFromJsonAsync<List<Category>>(_apiAdressCategory);
            ViewBag.CategoryId = new SelectList(list, "Id", "Name");
            var brand = await _httpClient.GetFromJsonAsync<List<Brand>>(_apiAdressBrand);
            ViewBag.BrandId = new SelectList(brand, "Id", "Name");
            return View(entity);
        }

        // GET: ProductsController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            var user = await _httpClient.GetFromJsonAsync<Product>(_apiAdress + "/" + id);
            if (user == null) return NotFound();
            var list = await _httpClient.GetFromJsonAsync<List<Category>>(_apiAdressCategory);
            ViewBag.CategoryId = new SelectList(list, "Id", "Name");
            var brand = await _httpClient.GetFromJsonAsync<List<Brand>>(_apiAdressBrand);
            ViewBag.BrandId = new SelectList(brand, "Id", "Name");
            return View(user);
        }

        // POST: BrandsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, Product entity, IFormFile? Image, bool? resmiSil)
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
            var list = await _httpClient.GetFromJsonAsync<List<Category>>(_apiAdressCategory);
            ViewBag.CategoryId = new SelectList(list, "Id", "Name");
            var brand = await _httpClient.GetFromJsonAsync<List<Brand>>(_apiAdressBrand);
            ViewBag.BrandId = new SelectList(brand, "Id", "Name");
            return View(entity);
        }

        // GET: BrandsController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var user = await _httpClient.GetFromJsonAsync<Product>(_apiAdress + "/" + id);
            if (user == null) return NotFound();
            var list = await _httpClient.GetFromJsonAsync<List<Category>>(_apiAdressCategory);
            ViewBag.CategoryId = new SelectList(list, "Id", "Name");
            var brand = await _httpClient.GetFromJsonAsync<List<Brand>>(_apiAdressBrand);
            ViewBag.BrandId = new SelectList(brand, "Id", "Name");
            return View(user);
        }

        // POST: BrandsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(int id, Product entity)
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
            var list = await _httpClient.GetFromJsonAsync<List<Category>>(_apiAdressCategory);
            ViewBag.CategoryId = new SelectList(list, "Id", "Name");
            var brand = await _httpClient.GetFromJsonAsync<List<Brand>>(_apiAdressBrand);
            ViewBag.BrandId = new SelectList(brand, "Id", "Name");
            return View(entity);
        }
    }
}
