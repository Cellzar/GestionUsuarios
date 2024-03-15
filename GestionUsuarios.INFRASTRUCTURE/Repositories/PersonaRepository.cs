using GestionUsuarios.APPLICATION.Common.Interfaces.Repository;
using GestionUsuarios.DOMAIN.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestionUsuarios.INFRASTRUCTURE.Repositories;

public class PersonaRepository : BaseRepository<Persona>, IPersonaRepository
{
    private readonly DbContext _dbContext;

    public PersonaRepository(DbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
