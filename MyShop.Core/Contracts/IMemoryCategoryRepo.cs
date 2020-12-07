using MyShop.Core.Models;
using System.Collections.Generic;

namespace MyShop.Core.Contracts
{
   public interface IMemoryCategoryRepo
    {
        void Commit();
        void Delete(string id);
        IEnumerable<ProductCategory> GetAll();
        ProductCategory GetById(string id);
        void Insert(ProductCategory category);
        void Update(ProductCategory productCategory);
    }
}