using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminPanelCRUD.Models
{
    public class Book
    {
        public int Id { get; set; }
        [StringLength(maximumLength:20)]
        public string Title { get; set; }
        [StringLength(maximumLength:40)]
        public string Description { get; set; }
        public string ImgUrl { get; set; }
        public double SalePrice { get; set; }
        public double Discount { get; set; }

        [NotMapped]
        public IFormFile? ImgFile { get; set; }
    }
}
