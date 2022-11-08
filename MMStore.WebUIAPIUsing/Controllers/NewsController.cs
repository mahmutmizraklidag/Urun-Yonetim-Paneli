using Microsoft.AspNetCore.Mvc;
using MMStore.Entities;

namespace MMStore.WebUIAPIUsing.Controllers
{
    public class NewsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiAdressNews;

        public NewsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apiAdressNews = "https://localhost:7231/api/News";
        }

        public async Task<IActionResult> IndexAsync()
        {
            var model = await _httpClient.GetFromJsonAsync<List<News>>(_apiAdressNews);
            return View(model);
        }
        public async Task<IActionResult> Detail(int id)
        {
            var model = await _httpClient.GetFromJsonAsync<News>(_apiAdressNews + "/" + id);
            return View(model);
        }
    }
}
