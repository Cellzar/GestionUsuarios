using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GestionUsuarios.APPLICATION.Common.Interfaces.Repository;

public interface IRepository<T> where T : class
{
    Task<List<T>> GetAll();
    Task<List<T>> GetAll(Expression<Func<T, bool>> predicate);
    Task<T?> GetById(int id);
    IEnumerable<T> Find(Expression<Func<T, bool>> expression, bool noTracking = true);
    Task<T?> Get(Expression<Func<T, bool>> predicate);
    Task Add(T entity);
    void Update(T entity);
    Task Delete(int id);
}
