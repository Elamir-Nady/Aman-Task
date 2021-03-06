using Data.Entites;
using Intrerfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Data.Context;

namespace Implementation
{
    public class GenericRepostory<T> : IGenericRepostory<T> where T : BaseModel
    {
        ApplicationDbContext Context;
        DbSet<T> Table;
        public GenericRepostory(ApplicationDbContext context)
        {
            Context = context;
            Table = Context.Set<T>();
        }

        public   IEnumerable<T> Get(string[] includes=null)
        {
            IQueryable<T> query = Table;
            if (includes != null)
            {
                foreach (var include in includes)
                    query = query.Include(include);

            }
    
          
           return query;
        }

        public T GetByID(int id, string[] includes = null)
        {
            IQueryable<T> query = Table;
            if (includes != null)
            {
                foreach (var include in includes)
                    query = query.Include(include);

            }

            return query.FirstOrDefault(i=>i.Id==id); 
        }

        public int Add(T entity)
        {
             Table.Add(entity);
            return entity.Id;
        }

        public void Update(T entity)
        {
            Table.Update(entity);
        }

        public void Remove(T entity)
        {
            Table.Remove(entity);
        }

       
    }
}
