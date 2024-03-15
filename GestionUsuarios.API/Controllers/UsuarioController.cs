using AutoMapper;
using GestionUsuarios.APPLICATION.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GestionUsuarios.API.Controllers;

public class UsuarioController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public UsuarioController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
}
