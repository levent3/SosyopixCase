using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Absract
{
    public interface IRepositoryBase<T> where T : class, new()
    {


        Task<IEnumerable<T>> GetAll();
        Task<int> CreateAsync(T entity);
        Task<int> UpdateAsync(T entity);
        Task<int> DeleteAsync(T entity);

        Task<T> FindAsync(Expression<Func<T, bool>> filter = null);


    }
}
