using Microsoft.AspNetCore.Mvc;
using MMStore.Entities;
using MMStore.Service.Repositories;
using MMStore.WebUI.Models;
using System.Diagnostics;

namespace MMStore.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository<Contact> _repositoryContact;
        private readonly IRepository<Slider> _repositorySlider;
        private readonly IRepository<Product> _repositoryProduct;

        public HomeController(ILogger<HomeController> logger, IRepository<Contact> repositoryContact, IRepository<Slider> repositorySlider, IRepository<Product> repositoryProduct)
        {
            _logger = logger;
            _repositoryContact = repositoryContact;
            _repositorySlider = repositorySlider;
            _repositoryProduct = repositoryProduct;
        }

        public async Task<IActionResult> Index()
        {
            var model = new HomePageViewModel();
            model.Products = await _repositoryProduct.GetAllAsync(c=>c.IsActive&&c.IsHome);
            model.Sliders = await _repositorySlider.GetAllAsync();
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [Route("AccessDenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }
        [Route("iletisim")]
        public IActionResult ContactUs()
        {
            return View();
        }
        [Route("iletisim"),HttpPost]
        public async Task<IActionResult> ContactUsAsync(Contact contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _repositoryContact.AddAsync(contact);
                    var message = await _repositoryContact.SaveChangesAsync();
                    if (message > 0)
                    {
                        TempData["mesaj"] = "<div class='alert alert-success'>Mesajınız İletilmiştir...Teşekkür Ederiz...</div>";
                        return RedirectToAction(nameof(ContactUs));
                    }
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
           
            return View(contact);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}