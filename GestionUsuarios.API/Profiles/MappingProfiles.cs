using AutoMapper;
using GestionUsuarios.DOMAIN.Dto;
using GestionUsuarios.DOMAIN.Entities;

namespace GestionUsuarios.API.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Persona, PersonaDto>();
        CreateMap<PersonaDto, Persona>();

        CreateMap<Usuario, UsuarioDto>();
        CreateMap<UsuarioDto, Usuario>();
    }
}
