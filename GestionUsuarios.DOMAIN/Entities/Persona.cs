namespace GestionUsuarios.DOMAIN.Entities;

public partial class Persona
{
    public int Identificador { get; set; }

    public string? Nombres { get; set; }

    public string? Apellidos { get; set; }

    public string? NumeroIdentificacion { get; set; }

    public string? Email { get; set; }

    public string? TipoIdentificacion { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public string NumeroIdentificacionConcatenado { get; set; } = null!;

    public string NombresApellidosConcatenados { get; set; } = null!;
}
