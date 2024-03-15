using GestionUsuarios.APPLICATION.Common.Interfaces;
using GestionUsuarios.APPLICATION.Common.Interfaces.Repository;
using GestionUsuarios.INFRASTRUCTURE.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace GestionUsuarios.INFRASTRUCTURE.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly BdContext _context;

    public UnitOfWork(BdContext context)
    {
        _context = context;
    }

    public IUsuarioRepository UsuarioRepository => new UsuarioRepository(_context);

    public IPersonaRepository PersonaRepository => new PersonaRepository(_context);

    public void SaveChanges()
    {
        _context.SaveChanges();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public DatabaseFacade Database
    {
        get { return _context.Database; }
    }
}
