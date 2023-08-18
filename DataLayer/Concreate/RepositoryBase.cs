using DataLayer.Absract;
using DataLayer.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Concreate
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class, new()
    {
        private SqlDbContext dbContext;

        public RepositoryBase()
        {

            dbContext = new SqlDbContext();
        }


        public virtual async Task<int> CreateAsync(T entity)
        {

            await dbContext.Set<T>().AddAsync(entity);

            return await dbContext.SaveChangesAsync();

            
        }

        public virtual async Task<int> DeleteAsync(T entity)
        {

            dbContext.Set<T>().Remove(entity);
            return await dbContext.SaveChangesAsync();
        }

        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> filter = null)
        {
            if (filter != null)
                return await dbContext.Set<T>().Where(filter).FirstOrDefaultAsync();
            else
                return await dbContext.Set<T>().FirstOrDefaultAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await dbContext.Set<T>().ToListAsync();
        }

        public virtual async Task<int> UpdateAsync(T entity)
        {
            dbContext.Set<T>().Update(entity);
            return await dbContext.SaveChangesAsync();
        }
    }
}
