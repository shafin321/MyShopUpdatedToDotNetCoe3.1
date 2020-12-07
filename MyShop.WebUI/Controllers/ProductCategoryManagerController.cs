using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyShop.Core.Contracts;
using MyShop.Core.Models;

namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        private IMemoryCategoryRepo _categoryRepo;

        public ProductCategoryManagerController(IMemoryCategoryRepo categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }
        public IActionResult Index()
        {
            var model = _categoryRepo.GetAll();
            return View(model);
        }
        
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductCategory productCategory)
        {
            if (!ModelState.IsValid)
            {
                return View(productCategory);
            }

            else
            {
                _categoryRepo.Insert(productCategory);
                _categoryRepo.Commit();

                return RedirectToAction("Index");
            }

        }

        public IActionResult Edit(string id)
        {
            var model = _categoryRepo.GetById(id);
            if (model == null)
            {
                return NotFound();
            }

            else
            {
                return View(model);
            }
        }
          
        [HttpPost]
        public IActionResult Edit(ProductCategory productCategory, string id)
        {
            var editToCategory = _categoryRepo.GetById(id);
            if (editToCategory == null)
            {
                return NotFound();
            }

            else
            {
                editToCategory.Id = productCategory.Id;
                editToCategory.Category = productCategory.Category;
                _categoryRepo.Commit();

                return RedirectToAction("Index");
                
            }

           
        }

        public IActionResult Delete(string id)
        {
            var model = _categoryRepo.GetById(id);
            if (model == null)
            {
                return NotFound();
            }
            else
            {
                return View(model);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(string id) {
            var deleteCategory = _categoryRepo.GetById(id);
            if (deleteCategory == null)
            {
                return NotFound();
            }
            else
            {
                _categoryRepo.Delete(id);
                return RedirectToAction();
            }

            
        
        }
    }
}
