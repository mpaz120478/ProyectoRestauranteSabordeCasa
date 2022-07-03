using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoRestauranteSabordeCasa.Models;
using Microsoft.AspNetCore.Authorization;
using RestauranteAPI;
using Grpc.Net.Client;

namespace ProyectoRestauranteSabordeCasa.Controllers
{
    [Authorize(Roles = "AD")]
    [Route("/[controller]/[action]")]
    public class EmpleadoController : Controller
    {
        private Empleados.EmpleadosClient _client;
        private Cargos.CargosClient _clientCar;

        public EmpleadoController()
        {
            var canal = GrpcChannel.ForAddress("https://localhost:7237");
            _client = new Empleados.EmpleadosClient(canal);
            _clientCar = new Cargos.CargosClient(canal);
        }

        

        [HttpGet]
        public async Task<IActionResult> Listado()
        {
            List<EmpleadoModel> lista = new List<EmpleadoModel>();
            var dato = await _client.ListarAsync(new EmptyEmp { });
            foreach (var item in dato.Item)
            {
                EmpleadoModel model = new EmpleadoModel();
                model.IdEmp = Convert.ToInt32(item.IdEmp);
                model.IdCargo = Convert.ToInt32(item.IdCargo);
                model.DniEmp = item.DniEmp;
                model.NombreEmp = item.NombreEmp;
                model.ApellidoEmp = item.ApellidoEmp;
                model.Telefono = item.TelefonoEmp;
                model.InicioContrato = Convert.ToDateTime(item.InicioContratoEmp);
                model.SueldoEmp = Convert.ToInt32(item.SueldoEmp);
                model.Activo = Convert.ToBoolean(item.ActivoEmp);
                model.NombreCargo = item.NombreCargo;
                lista.Add(model);
            }
            return View(lista);
        }

        [HttpGet]
        public async Task<IActionResult> Agregar()
        {
            ViewBag.cargos = new SelectList(await cargos(), "IdCargo", "NombreCargo");
            return View(new EmpleadoModel());
        }

        [HttpPost] 
        public async Task<IActionResult> Agregar(EmpleadoModel request)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.cargos = new SelectList(await cargos(), "IdCargo", "NombreCargo", request.IdCargo);
                return View(request);
            }
            var model = new Empleado();
            model.IdCargo = request.IdCargo.ToString();
            model.DniEmp = request.DniEmp;
            model.NombreEmp = request.NombreEmp;
            model.ApellidoEmp = request.ApellidoEmp;
            model.TelefonoEmp = request.Telefono;
            model.InicioContratoEmp = request.InicioContrato.ToString();
            model.SueldoEmp = request.SueldoEmp.ToString();
            model.ActivoEmp = request.Activo.ToString();
            var dato = await _client.AgregarAsync(model);
            ViewBag.mensaje = dato.Mensaje;
            ViewBag.cargos = new SelectList(await cargos(), "IdCargo", "NombreCargo", model.IdCargo);
            return View(request);
        }

        [HttpGet("{IdEmp}")]
        public async Task<IActionResult> Editar(int IdEmp)
        {
            EmpleadoModel empleado = await Buscar(IdEmp);
            if (empleado == null)
                return RedirectToAction("Listado");

            ViewBag.cargos = new SelectList(await cargos(), "IdCargo", "NombreCargo", empleado.IdCargo);
            return View(empleado);
        }

        [HttpPost("{IdEmp}")]
        public async Task<IActionResult> Editar(EmpleadoModel request)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.cargos = new SelectList(await cargos(), "IdCargo", "NombreCargo", request.IdCargo);
                return View(request);
            }
            var model = new Empleado();
            model.IdEmp = request.IdEmp.ToString();
            model.IdCargo = request.IdCargo.ToString();
            model.DniEmp = request.DniEmp;
            model.NombreEmp = request.NombreEmp;
            model.ApellidoEmp = request.ApellidoEmp;
            model.TelefonoEmp = request.Telefono;
            model.InicioContratoEmp = request.InicioContrato.ToString();
            model.SueldoEmp = request.SueldoEmp.ToString();
            model.ActivoEmp = request.Activo.ToString();
            var dato = await _client.EditarAsync(model);
            ViewBag.mensaje = dato.Mensaje;
            ViewBag.cargos = new SelectList(await cargos(), "IdCargo", "NombreCargo", model.IdCargo);
            return View(request);
        }

        [HttpGet("{IdEmp}")]
        public async Task<IActionResult> Eliminar(int IdEmp)
        {
            EmpleadoModel empleado = await Buscar(IdEmp);
            if (empleado == null)
                return RedirectToAction("Listado");

            ViewBag.cargos = new SelectList(await cargos(), "IdCargo", "NombreCargo", empleado.IdCargo);
            return View(empleado);
        }

        [HttpPost("{IdEmp}")]
        public async Task<IActionResult> Eliminar(EmpleadoModel request)
        {
            var Id = new EmpleadoId();
            Id.Id = request.IdEmp.ToString();
            var dato = await _client.EliminarAsync(Id);
            ViewBag.mensaje = dato.Mensaje;
            ViewBag.cargos = new SelectList(await cargos(), "IdCargo", "NombreCargo", 0);
            return View(new EmpleadoModel());
        }



        private async Task<IEnumerable<CargoModel>> cargos()
        {
            List<CargoModel> lista = new List<CargoModel>();
            var dato = await _clientCar.ListarAsync(new EmptyCar { });
            foreach (var item in dato.Item)
            {
                CargoModel model = new CargoModel();
                model.IdCargo = Convert.ToInt32(item.IdCargo);
                model.NombreCargo = item.NombreCargo;
                model.RolCargo = item.RolCargo;
                lista.Add(model);
            }
            return lista;
        }

        private async Task<EmpleadoModel> Buscar(int IdEmp)
        {
            EmpleadoModel model = new EmpleadoModel();
            var Id = new EmpleadoId();
            Id.Id = IdEmp.ToString();
            var dato = await _client.BuscarAsync(Id);
            model.IdEmp = Convert.ToInt32(dato.IdEmp);
            model.IdCargo = Convert.ToInt32(dato.IdCargo);
            model.DniEmp = dato.DniEmp;
            model.NombreEmp = dato.NombreEmp;
            model.ApellidoEmp = dato.ApellidoEmp;
            model.Telefono = dato.TelefonoEmp;
            model.InicioContrato = Convert.ToDateTime(dato.InicioContratoEmp);
            model.SueldoEmp = Convert.ToInt32(dato.SueldoEmp);
            model.Activo = Convert.ToBoolean(dato.ActivoEmp);
            model.NombreCargo = dato.NombreCargo;

            return model;
        }

    }
}