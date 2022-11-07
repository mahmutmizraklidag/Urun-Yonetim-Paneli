using Microsoft.AspNetCore.Mvc;
using MMStore.Entities;
using MMStore.WebUIAPIUsing.Models;
using System.Diagnostics;

namespace MMStore.WebUIAPIUsing.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiAdressContact;
        private readonly string _apiAdressSlider;
        private readonly string _apiAdressBrand;
        private readonly string _apiAdressNews;
        private readonly string _apiAdressProduct;
        public HomeController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apiAdressContact = "https://localhost:7231/api/Contacts";
            _apiAdressSlider = "https://localhost:7231/api/Sliders";
            _apiAdressBrand = "https://localhost:7231/api/Brands";
            _apiAdressNews = "https://localhost:7231/api/News";
            _apiAdressProduct = "https://localhost:7231/api/Products";
        }

        public async Task<IActionResult> Index()
        {
            var model = new HomePageViewModel();
            model.Sliders = await _httpClient.GetFromJsonAsync<List<Slider>>(_apiAdressSlider);
            model.Brands = await _httpClient.GetFromJsonAsync<List<Brand>>(_apiAdressBrand);
            model.Products = await _httpClient.GetFromJsonAsync<List<Product>>(_apiAdressProduct);
            model.News = await _httpClient.GetFromJsonAsync<List<News>>(_apiAdressNews);

            return View(model);
        }
        [Route("AccessDenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }
        
        public IActionResult ContactUs()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}