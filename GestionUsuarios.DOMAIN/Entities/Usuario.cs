using System;
using System.Collections.Generic;

namespace GestionUsuarios.DOMAIN.Entities;

public partial class Usuario
{
    public int Identificador { get; set; }

    public string? UsuarioNombre { get; set; }

    public string? Pass { get; set; }

    public DateTime FechaCreacion { get; set; }
}
