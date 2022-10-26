using MMStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMStore.Service.Repositories
{
    public interface IBrandRepository : IRepository<Brand>
    {
        Task<Brand> MarkalarıUrunleriyleGetir(int brandId);
    }
}
