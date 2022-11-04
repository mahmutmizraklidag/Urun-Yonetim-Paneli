using Microsoft.AspNetCore.Mvc;
using MMStore.Entities;
using MMStore.Service.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MMStore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IRepository<AppUser> _repository;

        public LoginController(IRepository<AppUser> repository)
        {
            _repository = repository;
        }
        // GET: api/<LoginController>
        [HttpGet]
        public async Task<ActionResult<AppUser>> GetAsync(string email,string password)
        {
            var data = await _repository.FirtOrDefaultAsync(u=>u.Email==email&&u.Password==password&&u.IsAdmin&&u.IsActive);
            if (data is null) return NotFound();
            return data;
        }

  
    }
}
