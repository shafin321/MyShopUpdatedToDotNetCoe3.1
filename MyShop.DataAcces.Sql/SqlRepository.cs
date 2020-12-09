using Microsoft.EntityFrameworkCore;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.DataAcces.Sql.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.DataAcces.Sql
{
    public class SqlRepository<T> : IMemoryGenericRepository<T> where T : BaseEntity
    {
        private ApplicationDbContext _context;
        private DbSet<T> dbSet;

        public SqlRepository(ApplicationDbContext context)
        {
            _context = context;
            this.dbSet = _context.Set<T>();
        }
        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Create(T t)
        {
            _context.Add(t);

        }

        public void Delete(string id)
        {
           
        }

        public IEnumerable<T> GetAll()
        {
            return dbSet;
        }

        public T GetById(string id)
        {
            return dbSet.Find(id);
        }

        public void Update(T t)
        {
            var model = dbSet.Find(t.Id);
            if(model != null)
            {
                //_context.Update(model);
                model = t;
                _context.SaveChanges();
            }

            else
            {
                throw new Exception("Not found");
            }
        }
    }
}
