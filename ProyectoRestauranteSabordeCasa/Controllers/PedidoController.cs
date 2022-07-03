using Grpc.Net.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoRestauranteSabordeCasa.Models;
using RestauranteAPI;

namespace ProyectoRestauranteSabordeCasa.Controllers
{
    [Authorize]
    [Route("/[controller]/[action]")]
    public class PedidoController : Controller
    {
        private Pedidos.PedidosClient _client;

        public PedidoController()
        {
            var canal = GrpcChannel.ForAddress("https://localhost:7237");
            _client = new Pedidos.PedidosClient(canal);
        }

        [HttpGet]
        [Authorize(Roles = "AD,CA,ME")]
        public async Task<IActionResult> Listado()
        {
            var dato = new Pedidoss();
            List<PedidoModel> lista = new List<PedidoModel>();
            if(User.FindFirst(x => x.Type == "Rol")?.Value == "ME")
            {
                dato = await _client.ListarPorUsuarioAsync(new PedidoId { Id = User.FindFirst(x => x.Type == "IdUser")?.Value });
            }
            else
            {
                dato = await _client.ListarAsync(new EmptyPed { });
            }

            foreach (var item in dato.Item)
            {
                PedidoModel model = new PedidoModel();
                model.IdPedido = Convert.ToInt32(item.IdPedido);
                model.IdUsuario = Convert.ToInt32(item.IdUsuario);
                model.MesaPedido = item.MesaPedido;
                model.FechaPedido = Convert.ToDateTime(item.FechaPedido);
                model.Pendiente = Convert.ToBoolean(item.Pendiente);
                lista.Add(model);
            }

            return View(lista);
        }

        [HttpGet]
        [Authorize(Roles = "ME")]
        public IActionResult Agregar()
        {
            return View(new PedidoModel());
        }

        [HttpPost]
        [Authorize(Roles = "ME")]
        public async Task<IActionResult> Agregar(PedidoModel request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var model = new Pedido();
            model.IdUsuario = User.FindFirst(x => x.Type == "IdUser")?.Value;
            model.MesaPedido = request.MesaPedido;
            model.FechaPedido = DateTime.Now.ToString("MM/dd/yyyy");
            var dato = await _client.AgregarAsync(model);
            ViewBag.mensaje = dato.Mensaje;
            return View(new PedidoModel());
        }

        [HttpGet("{Id}")]
        [Authorize(Roles = "ME")]
        public async Task<IActionResult> Editar(int Id)
        {
            PedidoModel Pedido = await Buscar(Id);
            if (Pedido == null)
                return RedirectToAction("Listado");

            return View(Pedido);
        }

        [HttpPost("{Id}")]
        [Authorize(Roles = "ME")]
        public async Task<IActionResult> Editar(PedidoModel request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var model = new Pedido();
            model.IdPedido = request.IdPedido.ToString();
            model.IdUsuario = User.FindFirst(x => x.Type == "IdUser")?.Value;
            model.MesaPedido = request.MesaPedido;
            model.FechaPedido = DateTime.Now.ToString("MM/dd/yyyy");
            var dato = await _client.EditarAsync(model);
            ViewBag.mensaje = dato.Mensaje;
            return View(request);
        }

        [HttpGet("{Id}")]
        [Authorize(Roles = "ME")]
        public async Task<IActionResult> Eliminar(int Id)
        {
            PedidoModel Pedido = await Buscar(Id);
            if (Pedido == null)
                return RedirectToAction("Listado");

            return View(Pedido);
        }

        [HttpPost("{Id}")]
        [Authorize(Roles = "ME")]
        public async Task<IActionResult> Eliminar(PedidoModel model)
        {
            var Id = new PedidoId();
            Id.Id = model.IdPedido.ToString();
            var dato = await _client.EliminarAsync(Id);
            ViewBag.mensaje = dato.Mensaje;
            return View(model);
        }




        private async Task<PedidoModel> Buscar(int IdPedido)
        {
            PedidoModel model = new PedidoModel();
            var Id = new PedidoId();
            Id.Id = IdPedido.ToString();
            var dato = await _client.BuscarAsync(Id);
            model.IdPedido = Convert.ToInt32(dato.IdPedido);
            model.IdUsuario = Convert.ToInt32(dato.IdUsuario);
            model.MesaPedido = dato.MesaPedido;
            model.FechaPedido = Convert.ToDateTime(dato.FechaPedido);
            model.Pendiente = Convert.ToBoolean(dato.Pendiente);

            return model;
        }
    }
}
