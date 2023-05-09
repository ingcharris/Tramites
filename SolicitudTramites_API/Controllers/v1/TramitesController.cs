using AutoMapper;
using SolicitudTramites_API.Datos;
using SolicitudTramites_API.Modelos;
using SolicitudTramites_API.Modelos.Dto;
using SolicitudTramites_API.Modelos.Especificaciones;
using SolicitudTramites_API.Repositorio.IRepositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace SolicitudTramites_API.Controllers.v1
{
    [Route("api/v{version:apiVersion}/Tramite")]
    [ApiController]
    [ApiVersion("1.0")]    
    public class TramitesController : ControllerBase
    {
        private readonly ILogger<TramitesController> _logger;
        private readonly ITramitesRepositorio _tramiteRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public TramitesController(ILogger<TramitesController> logger, ITramitesRepositorio tramiteRepo, IMapper mapper)
        {
            _logger = logger;
            _tramiteRepo = tramiteRepo;
            _mapper = mapper;
            _response = new();
        }


        [HttpGet]
        [ResponseCache(CacheProfileName = "Default30")]
        [Authorize(Roles ="admin", AuthenticationSchemes= "Bearer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetTramites()
        {
            try
            {
                _logger.LogInformation("Obtener los Trámites");

                IEnumerable<Tramites> tramiteList = await _tramiteRepo.ObtenerTodos();
                                
                _response.Resultado = _mapper.Map<IEnumerable<TramitesDto>>(tramiteList);
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

        [HttpGet("TramitesPaginado")]        
        [ResponseCache(CacheProfileName = "Default30")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<APIResponse> GetTramitesPaginado([FromQuery] Parametros parametros)
        {
            try
            {
                var tramiteList = _tramiteRepo.ObtenerTodosPaginado(parametros);
                _response.Resultado = _mapper.Map<IEnumerable<TramitesDto>>(tramiteList);
                _response.statusCode = HttpStatusCode.OK;
                _response.TotalPaginas = tramiteList.MetaData.TotalPages;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }


        [HttpGet("{id:int}", Name = "GetTramite")]
        [Authorize(Roles = "admin", AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetTramite(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al consultar el Trámite con Id " + id);
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.IsExitoso = false;
                    return BadRequest(_response);
                }
                // var tramite = TramitesStore.tramiteList.FirstOrDefault(v => v.Id == id);
                var tramite = await _tramiteRepo.Obtener(v => v.Id == id);

                if (tramite == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.IsExitoso = false;
                    return NotFound(_response);
                }

                _response.Resultado = _mapper.Map<TramitesDto>(tramite);
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
        [Authorize(Roles = "admin", AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CrearTramite([FromBody] TramitesCreateDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (await _tramiteRepo.Obtener(v => v.TramiteNombre.ToLower() == createDto.TramiteNombre.ToLower()) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "El trámite con este Nombre ya existe!");
                    return BadRequest(ModelState);
                }

                if (createDto == null)
                {
                    return BadRequest(createDto);
                }

                Tramites modelo = _mapper.Map<Tramites>(createDto);

                modelo.FechaCreacion = DateTime.Now;
                modelo.FechaActualizacion = DateTime.Now;
                await _tramiteRepo.Crear(modelo);
                _response.Resultado = modelo;
                _response.statusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetTramite", new { id = modelo.Id }, _response);
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
        public async Task<IActionResult> DeleteTramite(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var tramite = await _tramiteRepo.Obtener(v => v.Id == id);
                if (tramite == null)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                await _tramiteRepo.Remover(tramite);

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
        [Authorize(Roles = "admin", AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateTramite(int id, [FromBody] TramitesUpdateDto updateDto)
        {
            if (updateDto == null || id != updateDto.Id)
            {
                _response.IsExitoso = false;
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            Tramites modelo = _mapper.Map<Tramites>(updateDto);


            await _tramiteRepo.Actualizar(modelo);
            _response.statusCode = HttpStatusCode.NoContent;

            return Ok(_response);
        }


        [HttpPatch("{id:int}")]
        [Authorize(Roles = "admin", AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialTramite(int id, JsonPatchDocument<TramitesUpdateDto> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }
            var tramite = await _tramiteRepo.Obtener(v => v.Id == id, tracked: false);

            TramitesUpdateDto tramiteDto = _mapper.Map<TramitesUpdateDto>(tramite);


            if (tramite == null) return BadRequest();

            patchDto.ApplyTo(tramiteDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Tramites modelo = _mapper.Map<Tramites>(tramiteDto);


            await _tramiteRepo.Actualizar(modelo);
            _response.statusCode = HttpStatusCode.NoContent;

            return Ok(_response);
        }

    }
}
