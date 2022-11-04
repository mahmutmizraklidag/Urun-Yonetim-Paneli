using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MMStore.Entities;
using MMStore.WebUIAPIUsing.Models;
using System.Security.Claims;

namespace MMStore.WebUIAPIUsing.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiAdres;
        public LoginController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apiAdres = "https://localhost:7231/Api/Login";
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
                var account = await _httpClient.GetFromJsonAsync<AppUser>(_apiAdres + "?email=" + model.Email + "&password=" + model.Password);
                if (account == null)
                {
                    ModelState.AddModelError("", "Giriş Başarısız!");
                }
                else
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Email,account.Email),
                        new Claim("Role",account.IsAdmin?"Admin":"User"), //eğer adminse admin hakkı değilse user hakkı verdik.
                        new Claim("UserId",account.Id.ToString()),
                    };
                    var userIdentity = new ClaimsIdentity(claims, "Login");
                    ClaimsPrincipal principal = new(userIdentity);
                    await HttpContext.SignInAsync(principal);
                    return Redirect("/Admin/Main");
                }
            }
            catch (Exception hata)
            {
                ModelState.AddModelError("", hata.Message + "Hatası Oluştu!");

            }
            return View();
        }
        [Route("Admin/Logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync(); //çıkış yap
            return RedirectToAction(nameof(Index));  //logine yönlendir
        }

    }
}
