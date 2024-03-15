using GestionUsuarios.APPLICATION.Common.Interfaces.Repository;
using GestionUsuarios.DOMAIN.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestionUsuarios.INFRASTRUCTURE.Repositories;

public class TipoDocumentoRepository : BaseRepository<TipoDocumento>, ITipoDocumentoRepository
{
    private readonly DbContext _dbContext;

    public TipoDocumentoRepository(DbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
