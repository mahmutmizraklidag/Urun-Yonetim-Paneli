using MMStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMStore.Service.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> UrunuKategoriVeMarkaylaGetir(int productId);
    }
}
