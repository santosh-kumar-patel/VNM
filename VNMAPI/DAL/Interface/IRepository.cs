using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.Base;

namespace DAL.Interface
{
    public interface IRepository<T> where T : class, IEntity
    {
         Task<T> Add(T entity);
         Task<T> Update(T entity);
         Task<IEnumerable<T>> GetAll();
         Task<T> GetById(int Id);
         Task<T> Delete(int Id);
    }
}
