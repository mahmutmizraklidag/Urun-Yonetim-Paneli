using Microsoft.AspNetCore.Mvc;
using MMStore.Entities;
using MMStore.Service.Repositories;

namespace MMStore.WebUI.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository _repository;

        public CategoriesController(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index(int id)
        {
            var model =await _repository.GetCategoryByProduct(id);
            if (model == null) return NotFound();
        
            return View(model);
        }
    }
}
