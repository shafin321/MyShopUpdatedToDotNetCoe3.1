using MyShop.Core.Models;
using System.Collections.Generic;

namespace MyShop.Core.Contracts
{
 public   interface IMemoryGenericRepository<T> where T : BaseEntity
    {
        void Commit();
        void Create(T t);
        void Delete(string id);
        IEnumerable<T> GetAll();
        T GetById(string id);
        void Update(T t);
    }
}