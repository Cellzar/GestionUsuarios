namespace GestionUsuarios.DOMAIN.Entities;

public partial class TipoDocumento
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public bool? Activo { get; set; }
}

