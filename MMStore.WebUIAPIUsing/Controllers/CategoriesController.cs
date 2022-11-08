using Microsoft.AspNetCore.Mvc;
using MMStore.Entities;

namespace MMStore.WebUIAPIUsing.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiAdressCategory;
        public CategoriesController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apiAdressCategory = "https://localhost:7231/api/Categories";
        }
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null) return BadRequest();
            var product = await _httpClient.GetFromJsonAsync<Category>(_apiAdressCategory + "/" + id);
            if (product == null) return NotFound();
            return View(product);
        }
    }
}
