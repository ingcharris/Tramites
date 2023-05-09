using AutoMapper;
using SolicitudTramites_API.Datos;
using SolicitudTramites_API.Modelos;
using SolicitudTramites_API.Modelos.Dto;
using SolicitudTramites_API.Repositorio.IRepositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net;

namespace SolicitudTramites_API.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "admin", AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<APIResponse>> GetPredios()
        {
            try
            {
                _logger.LogInformation("Obtener Predios");

                IEnumerable<Predios> prediosList = await _prediosRepo.ObtenerTodos(incluirPropiedades: "Tramites");

                _response.Resultado = _mapper.Map<IEnumerable<PrediosDto>>(prediosList);
                _response.statusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet("{id:int}", Name = "GetPredios")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "admin", AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<APIResponse>> GetPredios(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al traer Predio con Id " + id);
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.IsExitoso = false;
                    return BadRequest(_response);
                }
                // var tramites = TramitesStore.tramitesList.FirstOrDefault(v => v.Id == id);
                var predios = await _prediosRepo.Obtener(v => v.PredioId == id, incluirPropiedades: "Tramites");

                if (predios == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.IsExitoso = false;
                    return NotFound(_response);
                }

                _response.Resultado = _mapper.Map<PrediosDto>(predios);
                _response.statusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "admin", AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<APIResponse>> CrearPredio([FromBody] PrediosCreateDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (await _prediosRepo.Obtener(v => v.PredioId == createDto.PredioId) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "El Id del Trámite ya existe!");
                    return BadRequest(ModelState);
                }

                if (await _tramitesRepo.Obtener(v => v.Id == createDto.TramiteId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "El Id del Trámite no existe!");
                    return BadRequest(ModelState);
                }

                if (createDto == null)
                {
                    return BadRequest(createDto);
                }

                Predios modelo = _mapper.Map<Predios>(createDto);

                modelo.FechaCreacion = DateTime.Now;
                modelo.FechaActualizacion = DateTime.Now;
                await _prediosRepo.Crear(modelo);
                _response.Resultado = modelo;
                _response.statusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetPredios", new { id = modelo.PredioId }, _response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "admin", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> DeletePredio(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var Predios = await _prediosRepo.Obtener(v => v.PredioId == id);
                if (Predios == null)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                await _prediosRepo.Remover(Predios);

                _response.statusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return BadRequest(_response);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "admin", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UpdatePredio(int id, [FromBody] PrediosUpdateDto updateDto)
        {
            if (updateDto == null || id != updateDto.PredioId)
            {
                _response.IsExitoso = false;
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            if (await _tramitesRepo.Obtener(V => V.Id == updateDto.TramiteId) == null)
            {
                ModelState.AddModelError("ErrorMessages", "El Id del Trámite No existe!");
                return BadRequest(ModelState);
            }

            Predios modelo = _mapper.Map<Predios>(updateDto);

            await _prediosRepo.Actualizar(modelo);
            _response.statusCode = HttpStatusCode.NoContent;

            return Ok(_response);
        }




    }
}
