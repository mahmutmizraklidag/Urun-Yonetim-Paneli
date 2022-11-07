using Microsoft.AspNetCore.Mvc;
using MMStore.Entities;

namespace MMStore.WebUIAPIUsing.Controllers
{
    public class ProductsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiAdressProduct;
        public ProductsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apiAdressProduct = "https://localhost:7231/api/Products";
        }

        
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return BadRequest();
            var product = await _httpClient.GetFromJsonAsync<Product>(_apiAdressProduct+"/"+id);
            if (product == null) return NotFound();
            return View(product);
        }
    }
}
