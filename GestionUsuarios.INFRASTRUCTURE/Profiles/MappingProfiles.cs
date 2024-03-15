using AutoMapper;
using GestionUsuarios.DOMAIN.Dto;
using GestionUsuarios.DOMAIN.Entities;

namespace GestionUsuarios.INFRASTRUCTURE.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Persona, PersonaDto>();
        CreateMap<PersonaDto, Persona>();
    }
}