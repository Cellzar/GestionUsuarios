using GestionUsuarios.APPLICATION.Common.Exceptions;
using GestionUsuarios.APPLICATION.Common.Interfaces.Repository;
using GestionUsuarios.INFRASTRUCTURE.Common.Static;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GestionUsuarios.INFRASTRUCTURE.Repositories;

public class BaseRepository<T> : IRepository<T> where T : class
{
    protected DbSet<T> _entities;

    public BaseRepository(DbContext dbContext)
    {
        this._entities = dbContext.Set<T>();
    }

    public async Task<List<T>> GetAll()
    {

        return await _entities.ToListAsync();
    }

    public async Task<List<T>> GetAll(Expression<Func<T, bool>> predicate)
    {

        return await _entities.Where(predicate).ToListAsync();
    }

    public async Task<T?> GetById(int id) => await _entities.FindAsync(id);

    public async Task<T?> Get(Expression<Func<T, bool>> predicate)
    {

        return await _entities.Where(predicate).FirstOrDefaultAsync();
    }

    public async Task Add(T entity)
    {

        await _entities.AddAsync(entity);
    }

    public async Task Delete(int id)
    {

        T? entity = await GetById(id);
        if (entity != null)
        {
            this._entities.Remove(entity);
        }
    }

    public void Update(T entity)
    {

        try
        {
            _entities.Update(entity);
        }
        catch (Exception ex)
        {
            throw new GeneralException($"{BaseRepositoryMessages.ERRBSRPSTR01}, ExMessage: {ex.Message}");
        }
    }
}