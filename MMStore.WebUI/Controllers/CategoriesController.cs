using Microsoft.AspNetCore.Mvc;

namespace MMStore.WebUI.Controllers
{
    public class CategoriesController : Controller
    {
        public IActionResult Index(int id)
        {
            return View();
        }
    }
}
