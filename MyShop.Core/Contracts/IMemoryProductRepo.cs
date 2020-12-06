using MyShop.Core.Models;
using System.Collections.Generic;

namespace MyShop.Core.Contracts

{
    public interface IMemoryProductRepo
    {
        void Commit();
        void Delete(string id);
        Product Find(string id);
        IEnumerable<Product> GetAll();
        void Insert(Product product);
        void Update(Product product);
    }
}