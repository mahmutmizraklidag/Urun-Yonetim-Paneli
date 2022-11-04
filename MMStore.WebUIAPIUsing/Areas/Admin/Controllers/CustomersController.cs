using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MMStore.Entities;

namespace MMStore.WebUIAPIUsing.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class CustomersController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiAdress;
        public CustomersController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apiAdress = "https://localhost:7231/api/Customers";
        }


        // GET: CustomersController
        public async Task<ActionResult> Index()
        {
            var model = await _httpClient.GetFromJsonAsync<List<Customer>>(_apiAdress);
            return View(model);
        }

        // GET: CustomersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CustomersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Customer entity)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _httpClient.PostAsJsonAsync(_apiAdress, entity);
                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index));
                    else ModelState.AddModelError("", "Kayıt Başarısız!");
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            return View(entity);
        }

        // GET: CustomersController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var contact = await _httpClient.GetFromJsonAsync<Customer>(_apiAdress + "/" + id);
            if (contact == null) return NotFound();
            return View(contact);
        }

        // POST: ContactsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Customer entity)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _httpClient.PutAsJsonAsync(_apiAdress + "/" + id, entity);
                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index));
                    else ModelState.AddModelError("", "Kayıt Başarısız!");
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }

            return View(entity);
        }

        // GET: CustomersController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var contact = await _httpClient.GetFromJsonAsync<Customer>(_apiAdress + "/" + id);
            if (contact == null) return NotFound();
            return View(contact);
        }

        // POST: ContactsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Customer entity)
        {
            try
            {
                var user = await _httpClient.DeleteAsync(_apiAdress + "/" + id);
                if (user.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
                else ModelState.AddModelError("", "Kayıt Silinemedi!");
            }
            catch
            {
                ModelState.AddModelError("", "Hata Oluştu!");
            }
            return View(entity);
        }
    }
}
