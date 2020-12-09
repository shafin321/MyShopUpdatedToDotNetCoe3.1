using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModel;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
       // private readonly IHostingEnvironment hostingEnvironment;
        private IMemoryGenericRepository<Product> _productRep; //Generic Repository
        //private IMemoryProductRepo _productRep;
        private IMemoryCategoryRepo _categoryRepo;
        public ProductManagerController(IMemoryGenericRepository<Product> productRep, IMemoryCategoryRepo categoryRepo)
        {
            _productRep = productRep;
            _categoryRepo = categoryRepo;
        //    this.hostingEnvironment=hostingEnvironment
           
        }
        public IActionResult Index()
        {
            IEnumerable<Product> products = _productRep.GetAll();
            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new Product();

            var viewModel = new ProductCategoryViewModel
            {
               
                ProductCategories = _categoryRepo.GetAll()

            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            else
            {


                _productRep.Create(product);

                _productRep.Commit();
                return RedirectToAction("Index");

            }
        }

        public IActionResult Edit(string id)
        {
            var product = _productRep.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            else
            {
                var viewmodel = new ProductCategoryViewModel
                {
                    Product = product,
                    ProductCategories = _categoryRepo.GetAll()
                };
                return View(viewmodel);
            }
        }

        [HttpPost]
        public IActionResult Edit(Product product, string id)
        {
            var productToEdit = _productRep.GetById(id);

            if (productToEdit == null)
            {
                return NotFound();
            }
            else
            {
                //updated to productEditId or mapping ...incoming product change will map to productToEdit to specific object
                productToEdit.Id = product.Id; //mapping from prdoucttoEdit.id to product.id
                productToEdit.Name = product.Name;
                productToEdit.Description = product.Description;
                productToEdit.Price = product.Price;
                productToEdit.Image = product.Image;
                productToEdit.Category = product.Category; //incoming product mapped to the productEdit 

                _productRep.Commit();
                return RedirectToAction("Index");

            }

        }

        public IActionResult Delete(string id)
        {
            var product = _productRep.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            else
            {
                return View(product);
            }


        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(string id)
        {
            var productToDelete = _productRep.GetById(id);
            if (productToDelete == null)
            {
                return NotFound();
            }

            else
            {
                _productRep.Delete(id);

                //     return View(productToDelete);

                return RedirectToAction("Index");

            }
        }


    }
}
