using AutoMapper;
using SolicitudTramites_Utilidad;
using SolicitudTramites_Web.Models;
using SolicitudTramites_Web.Models.Dto;
using SolicitudTramites_Web.Models.ViewModel;
using SolicitudTramites_Web.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Reflection;

namespace SolicitudTramites_Web.Controllers
{

    public class PrediosController : Controller
    {

        private readonly IPrediosService _prediosService;
        private readonly ITramitesService _tramitesService;
        private readonly IMapper _mapper;
        public PrediosController(IPrediosService prediosService, ITramitesService tramitesService, IMapper mapper)
        {
            _prediosService = prediosService;
            _tramitesService = tramitesService;
            _mapper = mapper;
        }


        [Authorize(Roles = "admin")]
        public async Task<IActionResult> IndexPredios()
        {

            List<PrediosDto> prediosList = new();

            var response = await _prediosService.ObtenerTodos<APIResponse>(HttpContext.Session.GetString(DS.SessionToken));

            if (response != null && response.IsExitoso)
            {
                prediosList = JsonConvert.DeserializeObject<List<PrediosDto>>(Convert.ToString(response.Resultado));
            }

            return View(prediosList);
        }


        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CrearPredio()
        {
            PrediosViewModel prediosVM = new();
            var response = await _tramitesService.ObtenerTodos<APIResponse>(HttpContext.Session.GetString(DS.SessionToken));
            if (response != null && response.IsExitoso)
            {
                prediosVM.TramitesList = JsonConvert.DeserializeObject<List<TramitesDto>>(Convert.ToString(response.Resultado))
                                          .Select(v => new SelectListItem
                                          {
                                              Text = v.TramiteNombre,
                                              Value = v.Id.ToString()
                                          });
            }

            return View(prediosVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearPredio(PrediosViewModel modelo)
        {
            if (ModelState.IsValid)
            {
                var response = await _prediosService.Crear<APIResponse>(modelo.Predios, HttpContext.Session.GetString(DS.SessionToken));
                if (response != null && response.IsExitoso)
                {
                    TempData["exitoso"] = "Predio creado exitosamente.";
                    return RedirectToAction(nameof(IndexPredios));
                }
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }


            var res = await _tramitesService.ObtenerTodos<APIResponse>(HttpContext.Session.GetString(DS.SessionToken));
            if (res != null && res.IsExitoso)
            {
                modelo.TramitesList = JsonConvert.DeserializeObject<List<TramitesDto>>(Convert.ToString(res.Resultado))
                                          .Select(v => new SelectListItem
                                          {
                                              Text = v.TramiteNombre,
                                              Value = v.Id.ToString()
                                          });
            }

            return View(modelo);
        }


        [Authorize(Roles = "admin")]
        public async Task<IActionResult> ActualizarPredio(int predioId)
        {
            PrediosUpdateViewModel prediosVM = new();

            var response = await _prediosService.Obtener<APIResponse>(predioId, HttpContext.Session.GetString(DS.SessionToken));
            if (response != null && response.IsExitoso)
            {
                PrediosDto modelo = JsonConvert.DeserializeObject<PrediosDto>(Convert.ToString(response.Resultado));
                prediosVM.Predios = _mapper.Map<PrediosUpdateDto>(modelo);
            }

            response = await _tramitesService.ObtenerTodos<APIResponse>(HttpContext.Session.GetString(DS.SessionToken));
            if (response != null && response.IsExitoso)
            {
                prediosVM.TramitesList = JsonConvert.DeserializeObject<List<TramitesDto>>(Convert.ToString(response.Resultado))
                                          .Select(v => new SelectListItem
                                          {
                                              Text = v.TramiteNombre,
                                              Value = v.Id.ToString()
                                          });
                return View(prediosVM);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActualizarPredio(PrediosUpdateViewModel modelo)
        {

            if (ModelState.IsValid)
            {
                var response = await _prediosService.Actualizar<APIResponse>(modelo.Predios, HttpContext.Session.GetString(DS.SessionToken));
                if (response != null && response.IsExitoso)
                {
                    TempData["exitoso"] = "Predio actualizado exitosamente.";
                    return RedirectToAction(nameof(IndexPredios));
                }
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }

            var res = await _tramitesService.ObtenerTodos<APIResponse>(HttpContext.Session.GetString(DS.SessionToken));
            if (res != null && res.IsExitoso)
            {
                modelo.TramitesList = JsonConvert.DeserializeObject<List<TramitesDto>>(Convert.ToString(res.Resultado))
                                          .Select(v => new SelectListItem
                                          {
                                              Text = v.TramiteNombre,
                                              Value = v.Id.ToString()
                                          });
            }

            return View(modelo);
        }


        [Authorize(Roles = "admin")]
        public async Task<IActionResult> RemoverPredio(int predioId)
        {
            PrediosDeleteViewModel prediosVM = new();

            var response = await _prediosService.Obtener<APIResponse>(predioId, HttpContext.Session.GetString(DS.SessionToken));
            if (response != null && response.IsExitoso)
            {
                PrediosDto modelo = JsonConvert.DeserializeObject<PrediosDto>(Convert.ToString(response.Resultado));
                prediosVM.Predios = modelo;
            }

            response = await _tramitesService.ObtenerTodos<APIResponse>(HttpContext.Session.GetString(DS.SessionToken));
            if (response != null && response.IsExitoso)
            {
                prediosVM.TramitesList = JsonConvert.DeserializeObject<List<TramitesDto>>(Convert.ToString(response.Resultado))
                                          .Select(v => new SelectListItem
                                          {
                                              Text = v.TramiteNombre,
                                              Value = v.Id.ToString()
                                          });
                return View(prediosVM);
            }

            return NotFound();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoverPredio(PrediosDeleteViewModel modelo)
        {
            var response = await _prediosService.Remover<APIResponse>(modelo.Predios.PredioId, HttpContext.Session.GetString(DS.SessionToken));
            if (response != null && response.IsExitoso)
            {
                TempData["exitoso"] = "Predio eliminado exitosamente.";
                return RedirectToAction(nameof(IndexPredios));
            }

            TempData["error"] = "Ocurrió un error al remover este registro.";
            return View(modelo);
        }
    }
}
