using Grpc.Net.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoRestauranteSabordeCasa.Models;
using RestauranteAPI;

namespace ProyectoRestauranteSabordeCasa.Controllers
{
    [Authorize]
    [Route("/[controller]/[action]")]
    public class DetallePedidoController : Controller
    {
        private DetallePedidos.DetallePedidosClient _client;
        private Productos.ProductosClient _clientPro;

        public DetallePedidoController()
        {
            var canal = GrpcChannel.ForAddress("https://localhost:7237");
            _clientPro = new Productos.ProductosClient(canal);
            _client = new DetallePedidos.DetallePedidosClient(canal);
        }

        [HttpGet]
        [Authorize(Roles = "AD")]
        public async Task<IActionResult> Listado()
        {
            List<DetallePedidoModel> lista = new List<DetallePedidoModel>();
            var dato = await _client.ListarAsync(new EmptyDet { });
            foreach (var item in dato.Item)
            {
                DetallePedidoModel model = new DetallePedidoModel();
                model.IdDetallePed = Convert.ToInt32(item.IdDetallePed);
                model.IdPedido = Convert.ToInt32(item.IdPedido);
                model.IdProd = Convert.ToInt32(item.IdProd);
                model.Cantidad = Convert.ToInt32(item.Cantidad);
                model.Observacion = item.Observacion;
                model.NombreProd = item.NombreProd;
                lista.Add(model);
            }

            return View(lista);
        }

        [HttpGet("{IdPedido}")]
        [Authorize(Roles = "AD,CA,ME")]
        public async Task<IActionResult> Listado(int IdPedido)
        {
            List<DetallePedidoModel> lista = new List<DetallePedidoModel>();
            var dato = await _client.ListarPorPedidoAsync(new DetallePedidoId { Id = IdPedido.ToString() });
            foreach (var item in dato.Item)
            {
                DetallePedidoModel model = new DetallePedidoModel();
                model.IdDetallePed = Convert.ToInt32(item.IdDetallePed);
                model.IdPedido = Convert.ToInt32(item.IdPedido);
                model.IdProd = Convert.ToInt32(item.IdProd);
                model.Cantidad = Convert.ToInt32(item.Cantidad);
                model.Observacion = item.Observacion;
                model.NombreProd = item.NombreProd;
                lista.Add(model);
            }

            ViewBag.IdPedido = IdPedido;
            return View(lista);
        }

        [HttpGet("{IdPedido}")]
        [Authorize(Roles = "ME")]
        public async Task<IActionResult> Agregar(int IdPedido)
        {
            ViewBag.IdPedido = IdPedido;
            ViewBag.productos = new SelectList(await productos(), "IdProd", "NombreProd");
            return View(new DetallePedidoModel());
        }

        [HttpPost("{IdPedido}")]
        [Authorize(Roles = "ME")]
        public async Task<IActionResult> Agregar(int IdPedido,DetallePedidoModel request)
        {
            ViewBag.IdPedido = IdPedido;
            ViewBag.productos = new SelectList(await productos(), "IdProd", "NombreProd");
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var model = new DetallePedido();
            model.IdPedido = IdPedido.ToString();
            model.IdProd = request.IdProd.ToString();
            model.Cantidad = request.Cantidad.ToString();
            model.Observacion = request.Observacion != null ?request.Observacion.ToString() : "";
            var dato = await _client.AgregarAsync(model);
            ViewBag.mensaje = dato.Mensaje;
            return View(new DetallePedidoModel());
        }

        [HttpGet("{IdPedido}/{Id}")]
        [Authorize(Roles = "ME")]
        public async Task<IActionResult> Editar(int IdPedido,int Id)
        {
            ViewBag.IdPedido = IdPedido;
            DetallePedidoModel model = await Buscar(Id);
            if (model == null)
                return RedirectToAction("Listado/"+ IdPedido);
            ViewBag.productos = new SelectList(await productos(), "IdProd", "NombreProd", model.IdProd);
            return View(model);
        }

        [HttpPost("{IdPedido}/{Id}")]
        [Authorize(Roles = "ME")]
        public async Task<IActionResult> Editar(int IdPedido,DetallePedidoModel request)
        {
            ViewBag.IdPedido = IdPedido;
            ViewBag.productos = new SelectList(await productos(), "IdProd", "NombreProd", request.IdProd);
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var model = new DetallePedido();
            model.IdDetallePed = request.IdDetallePed.ToString();
            model.IdPedido = IdPedido.ToString();
            model.IdProd = request.IdProd.ToString();
            model.Cantidad = request.Cantidad.ToString();
            model.Observacion = request.Observacion != null ? request.Observacion.ToString() : "";
            var dato = await _client.EditarAsync(model);
            ViewBag.mensaje = dato.Mensaje;
            return View(request);
        }

        [HttpGet("{IdPedido}/{Id}")]
        [Authorize(Roles = "ME")]
        public async Task<IActionResult> Eliminar(int IdPedido, int Id)
        {
            ViewBag.IdPedido = IdPedido;
            DetallePedidoModel model = await Buscar(Id);
            if (model == null)
                return RedirectToAction("Listado");

            ViewBag.productos = new SelectList(await productos(), "IdProd", "NombreProd", model.IdProd);
            return View(model);
        }

        [HttpPost("{Id}")]
        [Authorize(Roles = "ME")]
        public async Task<IActionResult> Eliminar(DetallePedidoModel model)
        {
            var Id = new DetallePedidoId();
            Id.Id = model.IdDetallePed.ToString();
            var dato = await _client.EliminarAsync(Id);
            ViewBag.mensaje = dato.Mensaje;
            return View(model);
        }


        private async Task<IEnumerable<ProductoModel>> productos()
        {
            List<ProductoModel> lista = new List<ProductoModel>();
            var dato = await _clientPro.ListarAsync(new EmptyProd { });
            foreach (var item in dato.Item)
            {
                ProductoModel model = new ProductoModel();
                model.IdProd = Convert.ToInt32(item.IdProd);
                model.IdTipoProd = Convert.ToInt32(item.IdTipoProd);
                model.NombreProd = item.NombreProd;
                model.Precio = Convert.ToDecimal(item.Precio);
                model.NombreTipoProd = item.NombreTipoProd;
                lista.Add(model);
            }
            return lista;
        }

        private async Task<DetallePedidoModel> Buscar(int IdDetallePedido)
        {
            DetallePedidoModel model = new DetallePedidoModel();
            var Id = new DetallePedidoId();
            Id.Id = IdDetallePedido.ToString();
            var dato = await _client.BuscarAsync(Id);
            model.IdDetallePed = Convert.ToInt32(dato.IdDetallePed);
            model.IdPedido = Convert.ToInt32(dato.IdPedido);
            model.IdProd = Convert.ToInt32(dato.IdProd);
            model.Cantidad = Convert.ToInt32(dato.Cantidad);
            model.Observacion = dato.Observacion;
            model.NombreProd = dato.NombreProd;

            return model;
        }
    }
}
