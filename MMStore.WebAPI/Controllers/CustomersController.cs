using Microsoft.AspNetCore.Mvc;
using MMStore.Entities;
using MMStore.Service.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MMStore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IRepository<Customer> _repository;

        public CustomersController(IRepository<Customer> repository)
        {
            _repository = repository;
        }
        // GET: api/<CustomersController>
        [HttpGet]
        public async Task<IEnumerable<Customer>> GetAsync()
        {
            return await _repository.GetAllAsync();
        }

        // GET api/<CustomersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetAsync(int id)
        {
            var data = await _repository.FindAsync(id);
            if (data is null) return NotFound();
            return data;
        }

        // POST api/<CustomersController>
        [HttpPost]
        public async Task<ActionResult<Customer>> PostAsync([FromBody] Customer entity)
        {
            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();
            return CreatedAtAction("Get", new { id = entity.Id }, entity);
        }

        // PUT api/<CustomersController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(int id, Customer entity)
        {
            _repository.Update(entity);
            await _repository.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/<CustomersController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var entity = _repository.Find(id);
            _repository.Delete(entity);
            await _repository.SaveChangesAsync();
            return Ok();
        }
    }
}
