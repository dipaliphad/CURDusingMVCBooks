using CURDusingMVCBooks.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CURDusingMVCBooks.Controllers
{
    public class BooksController : Controller
    {
        private readonly IConfiguration configuration;
        BooksDAL booksDAL;
        public BooksController(IConfiguration configuration)
        {
            this.configuration = configuration;
            booksDAL = new BooksDAL(this.configuration);

        }
        // GET: BooksController
        public ActionResult List()
        {
            ViewBag.BooksList = booksDAL.GetAllBooks();
            return View();
        }

        // GET: BooksController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BooksController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BooksController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(books books1)
        {
            try
            {
                int result = booksDAL.Addbooks(books1);
                if (result == 1)
                    return RedirectToAction(nameof(List));
                else
                    return BadRequest();
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET: BooksController/Edit/5
        public ActionResult Edit(int id)
        {
            var model = booksDAL.GetBooksById(id);
            return View(model);

        }

        // POST: BooksController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(books books1)
        {
            try
            {
                int result = booksDAL.Updatebooks(books1);
                if (result == 1)

                    return RedirectToAction(nameof(List));
                else
                    return BadRequest();
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET: BooksController/Delete/5
        public ActionResult Delete(int id)
        {
            var model = booksDAL.GetBooksById(id);
            return View(model);

        }

        // POST: BooksController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            try
            {
                int result = booksDAL.Deletebooks(id);
                if (result == 1)
                    return RedirectToAction(nameof(List));
                else
                    return BadRequest();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
