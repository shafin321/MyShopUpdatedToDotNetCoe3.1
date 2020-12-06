using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyShop.Core.Contracts;
using MyShop.Core.Models;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        private IMemoryProductRepo  _productRep;
        public ProductManagerController(IMemoryProductRepo productRepo)
        {
            _productRep = productRepo;
        }
        public IActionResult Index()
        {
            IEnumerable<Product> products = _productRep.GetAll();
            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
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
                _productRep.Insert(product);
                _productRep.Commit();

                return RedirectToAction("Index");
            }
            
        }
    }
}
