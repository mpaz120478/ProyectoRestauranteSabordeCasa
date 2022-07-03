using Grpc.Net.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoRestauranteSabordeCasa.Models;
using RestauranteAPI;

namespace ProyectoRestauranteSabordeCasa.Controllers
{
    [Authorize]
    [Route("/[controller]/[action]")]
    public class VentaController : Controller
    {
        private Ventas.VentasClient _client;
        private Productos.ProductosClient _clientPro;
        private DetallePedidos.DetallePedidosClient _clientPed;

        public VentaController()
        {
            var canal = GrpcChannel.ForAddress("https://localhost:7237");
            _client = new Ventas.VentasClient(canal);
            _clientPro = new Productos.ProductosClient(canal);
            _clientPed = new DetallePedidos.DetallePedidosClient(canal);
        }

        [HttpGet]
        [Authorize(Roles = "AD,CA")]
        public async Task<IActionResult> Listado()
        {
            var dato = new Ventass();
            List<VentaModel> lista = new List<VentaModel>();
            dato = await _client.ListarAsync(new EmptyVen { });

            foreach (var item in dato.Item)
            {
                VentaModel model = new VentaModel();
                model.IdVenta = Convert.ToInt32(item.IdVenta);
                model.IdPedido = Convert.ToInt32(item.IdPedido);
                model.IdUsuario = Convert.ToInt32(item.IdUsuario);
                model.IdCliente = Convert.ToInt32(item.IdCliente);
                model.PrecioBase = Convert.ToDecimal(item.PrecioBase);
                model.PrecioIgv = Convert.ToDecimal(item.PrecioIgv);
                model.PrecioTotal = Convert.ToDecimal(item.PrecioTotal);
                model.DniCliente = item.DniCliente;
                model.DatosCliente = item.DatosCliente;
                lista.Add(model);
            }

            return View(lista);
        }

        [HttpGet("{IdPedido}")]
        [Authorize(Roles = "CA")]
        public async Task<IActionResult> Agregar(string IdPedido)
        {
            ViewBag.IdPedido = IdPedido;
            var pedidos = await detPedidos(IdPedido);
            decimal PrecioTotal = 0;

            foreach (var item in pedidos)
            {
                var prod = await producto(item.IdProd);
                PrecioTotal += prod.Precio*item.Cantidad;
            }

            var model = new VentaModel()
            {
                IdPedido = Convert.ToInt32(IdPedido),
                PrecioBase = Math.Round(PrecioTotal / Convert.ToDecimal(1.18), 2),
                PrecioIgv = Math.Round(PrecioTotal - (PrecioTotal / Convert.ToDecimal(1.18)), 2),
                PrecioTotal = PrecioTotal
            };
            return View(model);
        }

        [HttpPost("{IdPedido}")]
        [Authorize(Roles = "CA")]
        public async Task<IActionResult> Agregar(VentaModel request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var model = new Venta();
            model.IdPedido = request.IdPedido.ToString();
            model.IdUsuario = User.FindFirst(x => x.Type == "IdUser")?.Value;
            model.DniCliente = request.DniCliente;
            model.DatosCliente = request.DatosCliente;
            model.PrecioBase = request.PrecioBase.ToString();
            model.PrecioIgv = request.PrecioIgv.ToString();
            model.PrecioTotal = request.PrecioTotal.ToString();
            var dato = await _client.AgregarAsync(model);
            ViewBag.mensaje = dato.Mensaje;
            return View(request);
        }


        private async Task<ProductoModel> producto(int IdProd)
        {
            ProductoModel model = new ProductoModel();
            var Id = new ProductoId();
            Id.Id = IdProd.ToString();
            var dato = await _clientPro.BuscarAsync(Id);
            model.IdProd = Convert.ToInt32(dato.IdProd);
            model.IdTipoProd = Convert.ToInt32(dato.IdTipoProd);
            model.NombreProd = dato.NombreProd;
            model.Precio = Convert.ToDecimal(dato.Precio);
            model.NombreTipoProd = dato.NombreTipoProd;

            return model;
        }

        private async Task<IEnumerable<DetallePedidoModel>> detPedidos(string IdPedido)
        {
            List<DetallePedidoModel> lista = new List<DetallePedidoModel>();
            var dato = await _clientPed.ListarPorPedidoAsync(new DetallePedidoId { Id = IdPedido });
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
            return lista;
        }
    }
}
