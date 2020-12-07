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
 public   class InMemoryGenericRepository<T> : IMemoryGenericRepository<T> where T : BaseEntity
    {
        ObjectCache cache = MemoryCache.Default;
        List<T> items;
        string className;

        public InMemoryGenericRepository()
        {
            //Getting actual name of our classs
            className = typeof(T).Name;

            items = cache[className] as List<T>;
            if (items == null)
            {
                items = new List<T>();
            }

        }

        public void Commit()
        {
            //saving items to cache 
            cache[className] = items;
        }

        public void Create(T t)
        {
            items.Add(t);
        }

        public void Update(T t)
        {
            var itemToUpdate = items.FirstOrDefault(i => i.Id == t.Id);
            if (itemToUpdate != null)
            {
                itemToUpdate = t;
            }

            else
            {
                throw new Exception("Not found");
            }

        }

        public IEnumerable<T> GetAll()
        {
            return items;
        }

        public T GetById(string id)
        {
            return items.FirstOrDefault(c => c.Id == id);
        }

        public void Delete(string id)
        {
            var itemToDelete = items.FirstOrDefault(d => d.Id == id);
            if (itemToDelete != null)
            {
                items.Remove(itemToDelete);


            }

            else
            {
                throw new Exception("not found");
            }

        }

    }
}
