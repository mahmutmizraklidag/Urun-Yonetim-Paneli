using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index()
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