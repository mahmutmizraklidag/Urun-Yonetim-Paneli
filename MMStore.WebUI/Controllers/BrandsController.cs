using Microsoft.AspNetCore.Mvc;
using MMStore.Service.Repositories;

namespace MMStore.WebUI.Controllers
{
    public class BrandsController : Controller
    {
        private readonly IBrandRepository _brandRepository;

        public BrandsController(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public async Task<IActionResult> IndexAsync(int id)
        {
            var brand=await _brandRepository.MarkalarıUrunleriyleGetir(id);
            if(brand==null) return NotFound();
            return View(brand);
        }
    }
}
