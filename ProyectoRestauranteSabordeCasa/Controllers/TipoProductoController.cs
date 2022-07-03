using Grpc.Net.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoRestauranteSabordeCasa.Models;
using RestauranteAPI;

namespace ProyectoRestauranteSabordeCasa.Controllers
{
    [Authorize(Roles = "AD")]
    [Route("/[controller]/[action]")]
    public class TipoProductoController : Controller
    {
        private TipoProductos.TipoProductosClient _client;

        public TipoProductoController()
        {
            var canal = GrpcChannel.ForAddress("https://localhost:7237");
            _client = new TipoProductos.TipoProductosClient(canal);
        }

        [HttpGet]
        public async Task<IActionResult> Listado()
        {
            List<TipoProductoModel> lista = new List<TipoProductoModel>();
            var dato = await _client.ListarAsync(new EmptyTip { });
            foreach (var item in dato.Item)
            {
                TipoProductoModel model = new TipoProductoModel();
                model.IdTipoProd = Convert.ToInt32(item.IdTipoProd);
                model.NombreTipoProd = item.NombreTipoProd;
                lista.Add(model);
            }

            return View(lista);
        }

        [HttpGet]
        public IActionResult Agregar()
        {
            return View(new TipoProductoModel());
        }

        [HttpPost]
        public async Task<IActionResult> Agregar(TipoProductoModel request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var model = new TipoProducto();
            model.NombreTipoProd = request.NombreTipoProd;
            var dato = await _client.AgregarAsync(model);
            ViewBag.mensaje = dato.Mensaje;
            return View(request);
        }

        [HttpGet("{IdTipoProd}")]
        public async Task<IActionResult> Editar(int IdTipoProd)
        {
            TipoProductoModel tipo = await Buscar(IdTipoProd);
            if (tipo == null)
                return RedirectToAction("Listado");

            return View(tipo);
        }

        [HttpPost("{IdTipoProd}")]
        public async Task<IActionResult> Editar(TipoProductoModel request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var model = new TipoProducto();
            model.IdTipoProd = request.IdTipoProd.ToString();
            model.NombreTipoProd = request.NombreTipoProd;
            var dato = await _client.EditarAsync(model);
            ViewBag.mensaje = dato.Mensaje;
            return View(request);
        }

        [HttpGet("{IdTipoProd}")]
        public async Task<IActionResult> Eliminar(int IdTipoProd)
        {
            TipoProductoModel tipo = await Buscar(IdTipoProd);
            if (tipo == null)
                return RedirectToAction("Listado");

            return View(tipo);
        }

        [HttpPost("{IdTipoProd}")]
        public async Task<IActionResult> Eliminar(TipoProductoModel model)
        {
            var Id = new TipoProductoId();
            Id.Id = model.IdTipoProd.ToString();
            var dato = await _client.EliminarAsync(Id);
            ViewBag.mensaje = dato.Mensaje;
            return View(model);
        }

        private async Task<TipoProductoModel> Buscar(int IdTipoProd)
        {
            TipoProductoModel model = new TipoProductoModel();
            var Id = new TipoProductoId();
            Id.Id = IdTipoProd.ToString();
            var dato = await _client.BuscarAsync(Id);
            model.IdTipoProd = Convert.ToInt32(dato.IdTipoProd);
            model.NombreTipoProd = dato.NombreTipoProd;

            return model;
        }
    }
}
