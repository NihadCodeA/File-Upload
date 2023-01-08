using Microsoft.EntityFrameworkCore;

namespace AdminPanelCRUD.Models
{
    public class PustokContext : DbContext
    {
        public PustokContext(DbContextOptions<PustokContext> options) : base(options){}

        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<BrandSlider> BrandSliders { get; set; }
        public DbSet<Book> Books { get; set; }

    }
}
