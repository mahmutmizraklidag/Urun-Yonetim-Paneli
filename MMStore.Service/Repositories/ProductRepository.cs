using Microsoft.EntityFrameworkCore;
using MMStore.Data;
using MMStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMStore.Service.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public async Task<Product> UrunuKategoriVeMarkaylaGetir(int productId)
        {
            return await _databaseContext.Products.Include(c => c.Brand).Include(c => c.Category).FirstOrDefaultAsync(c => c.Id == productId);
        }
    }
}
