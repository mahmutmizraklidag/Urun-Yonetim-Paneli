using MMStore.Entities;

namespace MMStore.Service.Repositories
{
    public interface IBrandRepository : IRepository<Brand>
    {
        Task<Brand> MarkalarıUrunleriyleGetir(int brandId);
    }
}
