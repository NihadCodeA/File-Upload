using AdminPanelCRUD.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanelCRUD.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class BookController : Controller
    {
        private readonly PustokContext _pustokContext;
        public BookController(PustokContext pustokContext)
        {
            _pustokContext = pustokContext;
        }
        public IActionResult Index()
        {
            List<Book> books = _pustokContext.Books.ToList();
            return View(books);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Book book)
        {
            string name = book.ImgFile.FileName;
            if (name.Length > 64)
            {
                name = name.Substring(name.Length - 64, 64);
            }
            name = Guid.NewGuid().ToString() + name;

            string path = "C:\\Users\\Nihad\\Desktop\\AdminPanelCRUD\\AdminPanelCRUD" +
                "\\wwwroot\\uploads\\books\\" + name;
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                book.ImgFile.CopyTo(fs);
            }
            book.ImgUrl = name;
            _pustokContext.Books.Add(book);
            _pustokContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            Book book = _pustokContext.Books.Find(id);
            if (book == null) View("Error");
            return View(book);
        }
        [HttpPost]
        public IActionResult Update(Book book)
        {
            Book existbook = _pustokContext.Books.Find(book.Id);
            if (book == null) View("Error");
            if (book.ImgUrl!= null)
            {
            string name = book.ImgFile.FileName;
                string path = "C:\\Users\\Nihad\\Desktop\\AdminPanelCRUD\\AdminPanelCRUD" +
              "\\wwwroot\\uploads\\books\\" + existbook.ImgUrl;
                FileInfo file = new FileInfo(path);
                if (file.Exists)
                {
                    file.Delete();
                }

                //-----------------------------------------------------------------------------
                if (name.Length > 64)
                {
                    name = name.Substring(name.Length - 64, 64);
                }
                name = Guid.NewGuid().ToString() + name;
                string newPath = "C:\\Users\\Nihad\\Desktop\\AdminPanelCRUD\\AdminPanelCRUD" +
                    "\\wwwroot\\uploads\\books\\" + name;
                using (FileStream fs = new FileStream(newPath, FileMode.Create))
                {
                    book.ImgFile.CopyTo(fs);
                }

                existbook.ImgUrl = name;
            }

            existbook.Title=book.Title;
            existbook.Description = book.Description;
            existbook.SalePrice = book.SalePrice;
            existbook.Discount=book.Discount;
            _pustokContext.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult Delete(int id)
        {
            Book book = _pustokContext.Books.Find(id);
            if (book == null) View("Error");
            return View(book);
        }
        [HttpPost]
        public IActionResult Delete(Book book)
        {
            Book existbook = _pustokContext.Books.Find(book.Id);
            if (existbook == null) View("Error");
            string path = "C:\\Users\\Nihad\\Desktop\\AdminPanelCRUD\\AdminPanelCRUD" +
               "\\wwwroot\\uploads\\books\\" + existbook.ImgUrl;
            FileInfo file = new FileInfo(path);
            if (file.Exists)
            {
                file.Delete();
            }
            //-----------------------------------------------------------------------------
            _pustokContext.Books.Remove(existbook);
            _pustokContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
