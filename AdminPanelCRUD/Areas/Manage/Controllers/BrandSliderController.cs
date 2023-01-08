using AdminPanelCRUD.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanelCRUD.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class BrandSliderController : Controller
    {
        private readonly PustokContext _pustokContext;
        public BrandSliderController(PustokContext pustokContext)
        {
            _pustokContext = pustokContext;
        }
        public IActionResult Index()
        {
            List<BrandSlider> brandSliderList = _pustokContext.BrandSliders.ToList();
            return View(brandSliderList);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(BrandSlider brandSlider)
        {
            string name = brandSlider.ImgFile.FileName;
            if (name.Length > 64)
            {
                name = name.Substring(name.Length - 64, 64);
            }
            name = Guid.NewGuid().ToString() + name;

            string path = "C:\\Users\\Nihad\\Desktop\\AdminPanelCRUD\\AdminPanelCRUD" +
                "\\wwwroot\\uploads\\brandsliders\\" + name;
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                brandSlider.ImgFile.CopyTo(fs);
            }
            brandSlider.ImgUrl = name;
            _pustokContext.BrandSliders.Add(brandSlider);
            _pustokContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            BrandSlider brandSlider = _pustokContext.BrandSliders.Find(id);
            if (brandSlider == null) View("Error");
            return View(brandSlider);
        }
        [HttpPost]
        public IActionResult Update(BrandSlider brandSlider)
        {
            BrandSlider existBrandSlider = _pustokContext.BrandSliders.Find(brandSlider.Id);
            if (brandSlider == null) View("Error");
            //if (!ModelState.IsValid) return View();
            string path = "C:\\Users\\Nihad\\Desktop\\AdminPanelCRUD\\AdminPanelCRUD" +
               "\\wwwroot\\uploads\\brandsliders\\" + existBrandSlider.ImgUrl;
            FileInfo file = new FileInfo(path);
            if (file.Exists)
            {
                file.Delete();
            }

            //-----------------------------------------------------------------------------
            string name = brandSlider.ImgFile.FileName;
            if (name.Length > 64)
            {
                name = name.Substring(name.Length - 64, 64);
            }
            name = Guid.NewGuid().ToString() + name;
            string newPath = "C:\\Users\\Nihad\\Desktop\\AdminPanelCRUD\\AdminPanelCRUD" +
                "\\wwwroot\\uploads\\brandsliders\\" + name;
            using (FileStream fs = new FileStream(newPath, FileMode.Create))
            {
                brandSlider.ImgFile.CopyTo(fs);
            }
            existBrandSlider.ImgUrl = name;
            _pustokContext.SaveChanges();
            return RedirectToAction("Index");
        }

        
        public IActionResult Delete(int id)
        {
            BrandSlider brandSlider = _pustokContext.BrandSliders.Find(id);
            if (brandSlider == null) View("Error");
            return View(brandSlider);
        }
        [HttpPost]
        public IActionResult Delete(BrandSlider brandSlider)
        {
            BrandSlider existBrandSlider = _pustokContext.BrandSliders.Find(brandSlider.Id);
            if (existBrandSlider == null) View("Error");
            string path = "C:\\Users\\Nihad\\Desktop\\AdminPanelCRUD\\AdminPanelCRUD" +
               "\\wwwroot\\uploads\\brandsliders\\" + existBrandSlider.ImgUrl;
            FileInfo file = new FileInfo(path);
            if (file.Exists)
            {
                file.Delete();
            }
            _pustokContext.BrandSliders.Remove(existBrandSlider);
            _pustokContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
