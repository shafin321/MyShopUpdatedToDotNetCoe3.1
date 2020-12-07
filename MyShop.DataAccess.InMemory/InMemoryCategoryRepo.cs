using MyShop.Core.Models;
using MyShop.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
  public  class InMemoryCategoryRepo : IMemoryCategoryRepo
    {
        ObjectCache cache = MemoryCache.Default;

        List<ProductCategory> productCategories;

        public InMemoryCategoryRepo()
        {
            productCategories = cache["productCategories"] as List<ProductCategory>;

            if (productCategories == null)
            {
                productCategories = new List<ProductCategory>();
            }

        }

        public void Commit()
        {
            cache["productCategories"] = productCategories; // save on cache
        }

        public void Insert(ProductCategory category)
        {
            productCategories.Add(category);
        }

        public void Update(ProductCategory productCategory)
        {
            var productToUpdate = productCategories.FirstOrDefault(c => c.Id == productCategory.Id);

            if (productToUpdate != null)
            {
                //updateing product->incoming product->productCategory
                productToUpdate = productCategory;
            }

            else
            {
                throw new Exception("Not found");
            }
        }

        public IEnumerable<ProductCategory> GetAll()
        {
            return productCategories;
        }

        public ProductCategory GetById(string id)
        {
            return productCategories.FirstOrDefault(c => c.Id == id);
        }

        public void Delete(string id)
        {
            var productToDelete = productCategories.FirstOrDefault(c => c.Id == id);
            if (productCategories != null)
            {
                productCategories.Remove(productToDelete);
            }
            else
            {
                throw new Exception("Not found");
            }

        }
    }
}
