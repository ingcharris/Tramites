using AutoMapper;
using SolicitudTramites_API.Datos;
using SolicitudTramites_API.Modelos;
using SolicitudTramites_API.Modelos.Dto;
using SolicitudTramites_API.Repositorio.IRepositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace SolicitudTramites_API.Controllers.v2
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class PrediosController : ControllerBase
    {
        private readonly ILogger<PrediosController> _logger;
        private readonly ITramitesRepositorio _tramitesRepo;
        private readonly IPrediosRepositorio _prediosRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public PrediosController(ILogger<PrediosController> logger, 
                                 ITramitesRepositorio tramitesRepo,
                                 IPrediosRepositorio prediosRepo, 
                                 IMapper mapper)
        {
            _logger = logger;
            _tramitesRepo = tramitesRepo;
            _prediosRepo = prediosRepo;
            _mapper = mapper;
            _response = new();
        }
       
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "valor1", "valor2" };
        }

    }
}
