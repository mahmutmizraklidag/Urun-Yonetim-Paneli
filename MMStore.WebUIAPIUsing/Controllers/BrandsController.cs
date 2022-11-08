using Microsoft.AspNetCore.Mvc;
using MMStore.Entities;

namespace MMStore.WebUIAPIUsing.Controllers
{
    public class BrandsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiAdressBrand;

        public BrandsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apiAdressBrand = "https://localhost:7231/api/Brands";
        }
        public async Task<IActionResult> IndexAsync()
        {
            var model = await _httpClient.GetFromJsonAsync<List<Brand>>(_apiAdressBrand);
            return View(model);
           
        }
        public async Task<IActionResult> Detail(int id)
        {
            var model = await _httpClient.GetFromJsonAsync<Brand>(_apiAdressBrand + "/" + id);
            return View(model);
        }
    }
}
