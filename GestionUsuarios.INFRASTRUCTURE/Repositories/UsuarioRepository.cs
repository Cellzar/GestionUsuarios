using GestionUsuarios.APPLICATION.Common.Interfaces.Repository;
using GestionUsuarios.DOMAIN.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestionUsuarios.INFRASTRUCTURE.Repositories;

public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
{
    private readonly DbContext _dbContext;

    public UsuarioRepository(DbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
