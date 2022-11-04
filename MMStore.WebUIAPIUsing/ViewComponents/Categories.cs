using Microsoft.AspNetCore.Mvc;
using MMStore.Entities;
using MMStore.Service.Repositories;

namespace MMStore.WebUIAPIUsing.ViewComponents
{
    public class Categories : ViewComponent
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiAdress;
        public Categories(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apiAdress = "https://localhost:7231/api/Categories";
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories =  await _httpClient.GetFromJsonAsync<List<Category>>(_apiAdress);
            return View(categories);
        }
       
    }
}
