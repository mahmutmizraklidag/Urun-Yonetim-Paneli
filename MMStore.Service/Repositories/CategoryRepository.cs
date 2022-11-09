using Microsoft.EntityFrameworkCore;
using MMStore.Data;
using MMStore.Entities;

namespace MMStore.Service.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public async Task<Category> GetCategoryByProduct(int categoryId)
        {
           return await _databaseContext.Categories.Include(c=>c.Products).FirstOrDefaultAsync(c=>c.Id== categoryId);
        }
    }
}
