using BulkyBookWeb.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using BulkyBookWeb.Models;
using System.Drawing;
using BulkyBookWeb.DataAccess.Repository.IRepository;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private IUnitOfWork unitOfWork {get; set;}

        public CategoryController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("categories")]
        public IActionResult Index()
        {
            IEnumerable<Category> Category = unitOfWork.category.GetAll();
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
                unitOfWork.category.Add(obj);
                unitOfWork.save();
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

            var categoryFromDb = unitOfWork.category.GetFirstOrDefault(i => i.Id == id);

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
                unitOfWork.category.Update(obj);
                unitOfWork.save();
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

            var categoryFromDb = unitOfWork.category.GetFirstOrDefault(u => u.Id == id);

            unitOfWork.category.Remove(categoryFromDb);
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
                var categoryObject = unitOfWork.category.GetFirstOrDefault(i => i.Id == id);
                if (categoryObject == null)
                {
                    return NotFound();
                }

                unitOfWork.category.Remove(categoryObject);
                unitOfWork.save();
                TempData["Success"] = "Category deleted successfully.";
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}
