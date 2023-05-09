using AutoMapper;
using SolicitudTramites_Utilidad;
using SolicitudTramites_Web.Models;
using SolicitudTramites_Web.Models.Dto;
using SolicitudTramites_Web.Models.ViewModel;
using SolicitudTramites_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace SolicitudTramites_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITramitesService _tramitesService;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, ITramitesService tramitesService, IMapper mapper)
        {
            _logger = logger;
            _tramitesService = tramitesService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int pageNumber=1)
        {
            List<TramitesDto> tramitesList = new();
            TramitesPaginadoViewModel tramitesVM = new TramitesPaginadoViewModel();

            if (pageNumber < 1) pageNumber = 1;

            var response = await _tramitesService.ObtenerTodosPaginado<APIResponse>(HttpContext.Session.GetString(DS.SessionToken), pageNumber, 4);

            if (response != null && response.IsExitoso)
            {
                tramitesList = JsonConvert.DeserializeObject<List<TramitesDto>>(Convert.ToString(response.Resultado));
                tramitesVM = new TramitesPaginadoViewModel()
                {
                    TramitesList = tramitesList,
                    PageNumber = pageNumber,
                    TotalPaginas = JsonConvert.DeserializeObject<int>(Convert.ToString(response.TotalPaginas))
                };

                if (pageNumber > 1) tramitesVM.Previo = "";
                if (tramitesVM.TotalPaginas <= pageNumber) tramitesVM.Siguiente = "disabled";

            }

            return View(tramitesVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}