using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.WebUI.Models;

namespace MyShop.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IMemoryGenericRepository<Product> _productRep; //Generic Repository
        //private IMemoryProductRepo _productRep;
        private IMemoryCategoryRepo _categoryRepo;
        public HomeController(ILogger<HomeController> logger, IMemoryGenericRepository<Product> productRep, IMemoryCategoryRepo categoryRepo)
        {
            _productRep = productRep;
            _categoryRepo = categoryRepo;
            _logger = logger;
        }

        public IActionResult Index() { 

          IEnumerable<Product> products = _productRep.GetAll();
            return View(products);

           
        }

        public IActionResult Details(string id)
        {
            var model = _productRep.GetById(id);
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
