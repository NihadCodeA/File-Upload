using AdminPanelCRUD.Models;

namespace AdminPanelCRUD.ViewModels
{
    public class HomeViewModel
    {
        public List<Slider> Sliders { get; set; }
        public List<Feature> Features { get; set; }
        public List<BrandSlider> BrandSliders { get; set; }
        public List<Book> Books { get; set; }
    }
}
