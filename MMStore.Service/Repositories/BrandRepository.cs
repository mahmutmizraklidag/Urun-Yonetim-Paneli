using Microsoft.EntityFrameworkCore;
using MMStore.Data;
using MMStore.Entities;

namespace MMStore.Service.Repositories
{
    public class BrandRepository : Repository<Brand>, IBrandRepository
    {
        public BrandRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public async Task<Brand> GetBrandByProduct(int brandId)
        {
            return await _databaseContext.Brands.Include(c => c.Products).FirstOrDefaultAsync(c => c.Id == brandId);
        }
    }
}
