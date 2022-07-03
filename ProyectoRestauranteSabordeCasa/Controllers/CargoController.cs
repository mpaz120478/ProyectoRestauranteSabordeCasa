using Grpc.Net.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoRestauranteSabordeCasa.Models;
using RestauranteAPI;

namespace ProyectoRestauranteSabordeCasa.Controllers
{
    [Authorize(Roles = "AD")]
    [Route("/[controller]/[action]")]
    public class CargoController : Controller
    {
        private Cargos.CargosClient _client;

        public CargoController()
        {
            var canal = GrpcChannel.ForAddress("https://localhost:7237");
            _client = new Cargos.CargosClient(canal);
        }

        [HttpGet]
        public async Task<IActionResult> Listado()
        {
            List<CargoModel> lista = new List<CargoModel>();
            var dato = await _client.ListarAsync(new EmptyCar { });
            foreach (var item in dato.Item)
            {
                CargoModel model = new CargoModel();
                model.IdCargo = Convert.ToInt32(item.IdCargo);
                model.NombreCargo = item.NombreCargo;
                model.RolCargo = item.RolCargo;
                lista.Add(model);
            }

            return View(lista);
        }

        [HttpGet]
        public IActionResult Agregar()
        {
            return View(new CargoModel());
        }

        [HttpPost]
        public async Task<IActionResult> Agregar(CargoModel request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            
            var model = new Cargo();
            model.NombreCargo = request.NombreCargo;
            model.RolCargo = request.RolCargo;
            var dato = await _client.AgregarAsync(model);
            ViewBag.mensaje = dato.Mensaje;
            return View(request);
        }

        [HttpGet("{IdCargo}")]
        public async Task<IActionResult> Editar(int IdCargo)
        {
            CargoModel cargo = await Buscar(IdCargo);
            if (cargo == null)
                return RedirectToAction("Listado");

            return View(cargo);
        }

        [HttpPost("{IdCargo}")]
        public async Task<IActionResult> Editar(CargoModel request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var model = new Cargo();
            model.IdCargo = request.IdCargo.ToString();
            model.NombreCargo = request.NombreCargo;
            model.RolCargo = request.RolCargo;
            var dato = await _client.EditarAsync(model);
            ViewBag.mensaje = dato.Mensaje;
            return View(request);
        }

        [HttpGet("{IdCargo}")]
        public async Task<IActionResult> Eliminar(int IdCargo)
        {
            CargoModel cargo = await Buscar(IdCargo);
            if (cargo == null)
                return RedirectToAction("Listado");

            return View(cargo);
        }

        [HttpPost("{IdCargo}")]
        public async Task<IActionResult> Eliminar(CargoModel model)
        {
            var Id = new CargoId();
            Id.Id = model.IdCargo.ToString();
            var dato = await _client.EliminarAsync(Id);
            ViewBag.mensaje = dato.Mensaje;
            return View(model);
        }




        private async Task<CargoModel> Buscar(int IdCargo)
        {
            CargoModel model = new CargoModel();
            var Id = new CargoId();
            Id.Id = IdCargo.ToString();
            var dato = await _client.BuscarAsync(Id);
            model.IdCargo = Convert.ToInt32(dato.IdCargo);
            model.NombreCargo = dato.NombreCargo;
            model.RolCargo = dato.RolCargo;
            
            return model;
        }
    }
}
