using MMStore.Entities;

namespace MMStore.WebUI.Models
{
    public class SearchViewModel
    {
        public IEnumerable<Brand> Brands { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public SearchViewModel()
        {
            Brands = new List<Brand>();
            Products = new List<Product>();
        }
    }
}
