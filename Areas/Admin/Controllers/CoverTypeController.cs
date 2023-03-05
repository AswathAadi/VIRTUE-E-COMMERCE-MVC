using BulkyBookWeb.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using BulkyBookWeb.Models;
using System.Drawing;
using BulkyBookWeb.DataAccess.Repository.IRepository;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private IUnitOfWork unitOfWork { get; set; }

        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("covertype")]
        public IActionResult Index()
        {
            IEnumerable<CoverType> Category = unitOfWork.CoverType.GetAll();
            return View(Category);
        }

        // GET - Create Category.
        [HttpGet]
        [Route("covertype/create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST - Create Category.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("covertype/create")]
        public IActionResult Create(CoverType obj)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.CoverType.Add(obj);
                unitOfWork.save();
                TempData["Success"] = "Cover Type created successfully.";
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET - Edit Category.
        [HttpGet]
        [Route("covertype/edit")]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var categoryFromDb = unitOfWork.CoverType.GetFirstOrDefault(i => i.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        // POST - Edit Category.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("covertype/edit")]
        public IActionResult Edit(CoverType obj)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.CoverType.Update(obj);
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

            var CoverTypeFromDb = unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);

            unitOfWork.CoverType.Remove(CoverTypeFromDb);
            if (CoverTypeFromDb == null)
            {
                return NotFound();
            }

            return View(CoverTypeFromDb);
        }

        // POST - Deleting Category
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            if (ModelState.IsValid)
            {
                var coverTypeObject = unitOfWork.CoverType.GetFirstOrDefault(i => i.Id == id);
                if (coverTypeObject == null)
                {
                    return NotFound();
                }

                unitOfWork.CoverType.Remove(coverTypeObject);
                unitOfWork.save();
                TempData["Success"] = "Cover Type deleted successfully.";
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}
