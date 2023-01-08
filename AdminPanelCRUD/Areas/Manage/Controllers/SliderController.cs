using AdminPanelCRUD.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace AdminPanelCRUD.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SliderController : Controller
    {
        private readonly PustokContext _pustokContext;
        public SliderController(PustokContext pustokContext)
        {
            _pustokContext = pustokContext;
        }
        public IActionResult Index()
        {
            List<Slider> sliderList = _pustokContext.Sliders.ToList();
            return View(sliderList);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Slider slider)
        {
            //if (!ModelState.IsValid) return View();
            //if(slider.ImgFile.ContentType!= "image/png,jpg,jpeg")
            //{
            //    ModelState.AddModelError("ImgFile","Ancaq png ve jpg(jpeg) ola biler!");
            //    return View();
            //}

            string name = slider.ImgFile.FileName;
            if (name.Length>64)
            {
                name = name.Substring(name.Length-64,64);
            }
            name = Guid.NewGuid().ToString() + name;

            string path = "C:\\Users\\Nihad\\Desktop\\AdminPanelCRUD\\AdminPanelCRUD" +
                "\\wwwroot\\uploads\\sliders\\"+name;
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                slider.ImgFile.CopyTo(fs);
            }
            slider.ImgUrl = name;
            _pustokContext.Sliders.Add(slider);
            _pustokContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            Slider slider = _pustokContext.Sliders.Find(id);
            if (slider == null) View("Error");
            return View(slider);
        }
        [HttpPost]
        public IActionResult Update(Slider slider)
        {
            //if (!ModelState.IsValid) return View();
            Slider existSlider = _pustokContext.Sliders.Find(slider.Id);
            if (slider == null) View("Error");
            if (slider.ImgUrl!=null)
            {
                string path = "C:\\Users\\Nihad\\Desktop\\AdminPanelCRUD\\AdminPanelCRUD" +
                "\\wwwroot\\uploads\\sliders\\" + existSlider.ImgUrl;
                FileInfo file = new FileInfo(path);
                if (file.Exists)
                {
                    file.Delete();
                }

                //-----------------------------------------------------------------------------
                string name = slider.ImgFile.FileName;
                if (name.Length > 64)
                {
                    name = name.Substring(name.Length - 64, 64);
                }
                name = Guid.NewGuid().ToString() + name;
                string newPath = "C:\\Users\\Nihad\\Desktop\\AdminPanelCRUD\\AdminPanelCRUD" +
                    "\\wwwroot\\uploads\\sliders\\" + name;
                using (FileStream fs = new FileStream(newPath, FileMode.Create))
                {
                    slider.ImgFile.CopyTo(fs);
                }
                existSlider.ImgUrl = name;
            }
            existSlider.FirstTitle = slider.FirstTitle;
            existSlider.SecondTitle = slider.SecondTitle;
            existSlider.Description = slider.Description;
            existSlider.RedirectUrl = slider.RedirectUrl;
            existSlider.RedirectUrlText = slider.RedirectUrlText;
            existSlider.Order = slider.Order;
            _pustokContext.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult Delete(int id)
        {
            Slider slider = _pustokContext.Sliders.Find(id);
            if (slider == null) View("Error");
            return View(slider);
        }
        [HttpPost]
        public IActionResult Delete(Slider slider)
        {
            Slider existSlider = _pustokContext.Sliders.Find(slider.Id);
            if (existSlider == null) View("Error");
            string path = "C:\\Users\\Nihad\\Desktop\\AdminPanelCRUD\\AdminPanelCRUD" +
                "\\wwwroot\\uploads\\sliders\\" + existSlider.ImgUrl;
            FileInfo file = new FileInfo(path);
            if (file.Exists)
            {
                file.Delete();
            }
            _pustokContext.Sliders.Remove(existSlider);
            _pustokContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
