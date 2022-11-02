using Microsoft.AspNetCore.Mvc;
using MMStore.Entities;
using MMStore.Service.Repositories;
using NuGet.Protocol.Core.Types;

namespace MMStore.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IRepository<Customer> _repository;

        public AccountController(IRepository<Customer> repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignIn(string email,string password)
        {
            TempData["Mesaj"] = email+"-"+password;
            return View();
        }
        public IActionResult SignUp()
        {
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(Customer customer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.AddAsync(customer);
                    var model = await _repository.SaveChangesAsync();
                    if (model > 0)
                    {
                        TempData["Mesaj"] = "<div class='alert alert-success'>Kayıdınız başarıyla oluşturuldu...</div>";
                    }
                }
                catch
                {

                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }


            return RedirectToAction(nameof(SignUp));
        }
        public IActionResult SignOut()
        {
            return View();
        }
    }
}
