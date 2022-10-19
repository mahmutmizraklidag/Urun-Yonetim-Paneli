using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MMStore.Entities;
using MMStore.Service.Repositories;
using MMStore.WebUI.Models;
using System.Security.Claims;

namespace MMStore.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private readonly IRepository<AppUser> _repository;

        public LoginController(IRepository<AppUser> repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> IndexAsync(AdminLoginViewModel model)
        {
            try
            {
                var account = _repository.Get(x => x.Email == model.Email && x.Password == model.Password && x.IsActive);
                if (account == null)
                {
                    ModelState.AddModelError("", "Giriş Başarısız");
                }
                else
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Email,account.Email),
                        new Claim("Role",account.IsAdmin ? "Admin":"User"),
                        new Claim("UserId",account.Id.ToString()),
                    };
                    var userIdentiy = new ClaimsIdentity(claims, "Login");
                    ClaimsPrincipal principal = new(userIdentiy);
                    await HttpContext.SignInAsync(principal);
                    return Redirect("/Admin/Main");
                }
            }
            catch (Exception hata)
            {
                ModelState.AddModelError("", hata.Message + "Hatası Oluştu");
            }
            return View();
        }
        [Route("Admin/Logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
