using AutoMapper;
using SolicitudTramites_Utilidad;
using SolicitudTramites_Web.Models;
using SolicitudTramites_Web.Models.Dto;
using SolicitudTramites_Web.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace SolicitudTramites_Web.Controllers
{
    
    public class TramitesController : Controller
    {
        private readonly ITramitesService _tramitesService;
        private readonly IMapper _mapper;

        public TramitesController(ITramitesService tramitesService, IMapper mapper)
        {
            _tramitesService= tramitesService;
            _mapper= mapper;
        }

        [Authorize(Roles ="admin")]
        public async Task<IActionResult> IndexTramites()
        {

            List<TramitesDto> tramitesList = new();

            var response = await _tramitesService.ObtenerTodos<APIResponse>(HttpContext.Session.GetString(DS.SessionToken));

            if(response != null  && response.IsExitoso) {
                tramitesList = JsonConvert.DeserializeObject<List<TramitesDto>>(Convert.ToString(response.Resultado));
            }

            return View(tramitesList);
        }

        //Get

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CrearTramite()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearTramite(TramitesCreateDto modelo)
        {
            if(ModelState.IsValid)
            {
                var response = await _tramitesService.Crear<APIResponse>(modelo, HttpContext.Session.GetString(DS.SessionToken));

                if(response != null && response.IsExitoso) {

                    TempData["exitoso"] = "Trámite creado exitosamente.";
                    return RedirectToAction(nameof(IndexTramites));
                }
            }
            return View(modelo);
        }


        [Authorize(Roles = "admin")]
        public async Task<IActionResult> ActualizarTramite(int tramiteId)
        {
            var response = await _tramitesService.Obtener<APIResponse>(tramiteId, HttpContext.Session.GetString(DS.SessionToken));

            if(response != null && response.IsExitoso)
            {
                TramitesDto model = JsonConvert.DeserializeObject<TramitesDto>(Convert.ToString(response.Resultado));
                return View(_mapper.Map<TramitesUpdateDto>(model));
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActualizarTramite(TramitesUpdateDto modelo)
        {
            if(ModelState.IsValid)
            {
                var response = await _tramitesService.Actualizar<APIResponse>(modelo, HttpContext.Session.GetString(DS.SessionToken));

                if(response !=null && response.IsExitoso)
                {
                    TempData["exitoso"] = "Trámite actualizado exitosamente.";
                    return RedirectToAction(nameof(IndexTramites));
                }
            }
            return View(modelo);
        }


        [Authorize(Roles = "admin")]
        public async Task<IActionResult> RemoverTramite(int tramiteId)
        {
            var response = await _tramitesService.Obtener<APIResponse>(tramiteId, HttpContext.Session.GetString(DS.SessionToken));

            if (response != null && response.IsExitoso)
            {
                TramitesDto model = JsonConvert.DeserializeObject<TramitesDto>(Convert.ToString(response.Resultado));
                return View(model);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoverTramite(TramitesDto modelo)
        {  
            var response = await _tramitesService.Remover<APIResponse>(modelo.Id, HttpContext.Session.GetString(DS.SessionToken));

            if (response != null && response.IsExitoso)
            {
                TempData["exitoso"] = "Trámite eliminado exitosamente.";
                return RedirectToAction(nameof(IndexTramites));
            }
            TempData["error"] = "Ocurrió un error al remover este registro.";
            return View(modelo);
        }
    }
}
