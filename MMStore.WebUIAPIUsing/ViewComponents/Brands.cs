using Microsoft.AspNetCore.Mvc;
using MMStore.Entities;
using MMStore.Service.Repositories;

namespace MMStore.WebUIAPIUsing.ViewComponents
{
    public class Brands : ViewComponent
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiAdress;
        public Brands(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apiAdress = "https://localhost:7231/api/Brands";
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var brands = await _httpClient.GetFromJsonAsync<List<Brand>>(_apiAdress);
            return View(brands);
        }
    }
}
