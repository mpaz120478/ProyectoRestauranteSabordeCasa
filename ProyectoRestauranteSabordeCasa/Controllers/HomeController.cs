using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoRestauranteSabordeCasa.Models;
using System.Diagnostics;

namespace ProyectoRestauranteSabordeCasa.Controllers
{
    [Authorize] //indica q si no està autorizado no puede acceder a los métodos del controlador
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        /*public IActionResult CerrarSesion()
        {
          //  HttpContext.Session.SetString(SesionUsuario, oUsuario.NombreUsuario);
            //return RedirectToAction("Inicio", "Login");
        }*/


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}