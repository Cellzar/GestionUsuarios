using GestionUsuarios.APPLICATION.Common.Interfaces.Repository;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionUsuarios.APPLICATION.Common.Interfaces;

public interface IUnitOfWork
{
    void SaveChanges();
    Task<int> SaveChangesAsync();
    DatabaseFacade Database { get; }
    IUsuarioRepository UsuarioRepository { get; }
    IPersonaRepository PersonaRepository { get; }
    ITipoDocumentoRepository TipoDocumentoRepository { get; }
}
