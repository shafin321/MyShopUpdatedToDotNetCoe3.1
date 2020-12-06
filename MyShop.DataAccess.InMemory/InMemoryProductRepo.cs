using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class InMemoryProductRepo : IMemoryProductRepo
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> products;

        public InMemoryProductRepo()
        {
            
            products = cache["products"] as List<Product>;


            // if its not found the product list in cache then it will create list of product 
            //if product cache is null then it will create new list of product from the inital constructor
            if (products == null)
            {
                products = new List<Product>();
            }



        }

        public void Commit()
        {
            //commit method  store prodcuts in chache 
            cache["products"] = products;
        }

        public void Insert(Product product)
        {
            products.Add(product);
        }

        //compy the info the product we send

        public void Update(Product product)
        {
            // get product id 
            Product ProductToUpdate = products.FirstOrDefault(p => p.Id == product.Id);
            if (ProductToUpdate != null)
            {
                ProductToUpdate = product; // comping the recving product 
            }
            else
            {
                throw new Exception("product not found");
            }
        }

        public Product Find(string id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);

            if (product != null)
            {
                return product;
            }

            else
            {
                throw new Exception("not found");
            }
        }

        public IEnumerable<Product> GetAll()
        {
            return products;
        }

        public void Delete(string id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);

            if (product != null)
            {
                products.Remove(product);
            }

            else
            {
                throw new Exception("not found");
            }
        }

    }
}
