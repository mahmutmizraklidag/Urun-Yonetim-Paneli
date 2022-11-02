using Microsoft.AspNetCore.Mvc;
using MMStore.Entities;
using MMStore.Service.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MMStore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlidersController : ControllerBase
    {
        private readonly IRepository<Slider> _repository;

        public SlidersController(IRepository<Slider> repository)
        {
            _repository = repository;
        }
        // GET: api/<SlidersController>
        [HttpGet]
        public async Task<IEnumerable<Slider>> GetAsync()
        {
            return await _repository.GetAllAsync();
        }

        // GET api/<SlidersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Slider>> GetAsync(int id)
        {
            var data = await _repository.FindAsync(id);
            if (data is null) return NotFound();
            return data;
        }

        // POST api/<SlidersController>
        [HttpPost]
        public async Task<ActionResult<Slider>> PostAsync([FromBody] Slider entity)
        {
            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();
            return CreatedAtAction("Get", new { id = entity.Id }, entity);
        }

        // PUT api/<SlidersController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(int id, Slider entity)
        {
            _repository.Update(entity);
            await _repository.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/<SlidersController>/5
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
