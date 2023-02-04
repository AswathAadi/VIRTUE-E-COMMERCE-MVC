using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Models;
using System.Drawing;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private ApplicationDbContext context {get; set;}

        public CategoryController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [Route("categories")]
        public IActionResult Index()
        {
            IEnumerable<Category> Category = context.categories;
            return View(Category);
        }

        // GET - Create Category.
        [HttpGet]
        [Route("category/create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST - Create Category.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("category/create")]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                context.categories.Add(obj);
                context.SaveChanges();
                TempData["Success"] = "Category created successfully.";
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET - Edit Category.
        [HttpGet]
        [Route("category/edit")]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var categoryFromDb = context.categories.Where(i => i.Id == id).FirstOrDefault();

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        // POST - Edit Category.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("category/edit")]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                context.categories.Update(obj);
                context.SaveChanges();
                TempData["Success"] = "Category edited successfully.";
                return RedirectToAction("Index");
            }

            return View();
        }


        // GET - Deleting Category
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var categoryFromDb = context.categories.Where(i => i.Id == id).FirstOrDefault();

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        // POST - Deleting Category
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            if (ModelState.IsValid)
            {
                var categoryObject = context.categories.Where(i => i.Id == id).FirstOrDefault();
                if (categoryObject == null)
                {
                    return NotFound();
                }

                context.categories.Remove(categoryObject);                
                context.SaveChanges();
                TempData["Success"] = "Category deleted successfully.";
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}
