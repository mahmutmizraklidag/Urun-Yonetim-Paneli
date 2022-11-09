using Microsoft.AspNetCore.Mvc;
using MMStore.Entities;
using MMStore.Service.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MMStore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;

        public ProductsController(IProductRepository repository)
        {
            _repository = repository;
        }
        // GET: api/<ProductsController>
        [HttpGet]
        public async Task<IEnumerable<Product>> GetAsync()
        {
            return await _repository.GetProductsByCategoryAndBrand();
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetAsync(int id)
        {
            var data = await _repository.FindAsync(id);
            if (data is null) return NotFound();
            return data;
        }
        [HttpGet("GetGetProductByCategoryAndBrand/{id}")]
        public async Task<ActionResult<Product>> GetGetProductByCategoryAndBrand(int id)
        {
            var data = await _repository.GetProductByCategoryAndBrand(id);
            if (data is null) return NotFound();
            return data;
        }

        // POST api/<ProductsController>
        [HttpPost]
        public async Task<ActionResult<Product>> PostAsync([FromBody] Product entity)
        {
            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();
            return CreatedAtAction("Get", new { id = entity.Id }, entity);
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(int id, Product entity)
        {
            _repository.Update(entity);
            await _repository.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/<ProductsController>/5
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
