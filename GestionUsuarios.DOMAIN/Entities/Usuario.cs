using System;
using System.Collections.Generic;

namespace GestionUsuarios.DOMAIN.Entities;

public partial class Usuario
{
    public int Identificador { get; set; }

    public string? Usuario1 { get; set; }

    public string? Pass { get; set; }

    public DateOnly? FechaCreacion { get; set; }
}
