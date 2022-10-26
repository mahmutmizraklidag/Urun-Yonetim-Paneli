using MMStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMStore.Service.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<Category> KategoriyiUrunleriyleGetir(int categoryId);
    }
}
