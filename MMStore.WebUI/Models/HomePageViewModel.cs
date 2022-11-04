using MMStore.Entities;

namespace MMStore.WebUI.Models
{
    public class HomePageViewModel
    {
        public IEnumerable<Slider> Sliders { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Brand> Brands { get; set; }
        public IEnumerable<News> News { get; set; }
        public HomePageViewModel()
        {
            Sliders = new List<Slider>();
            Products = new List<Product>();
            Brands = new List<Brand>();
            News = new List<News>();
        }
    }
}
