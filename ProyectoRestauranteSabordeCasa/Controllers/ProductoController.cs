using Grpc.Net.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoRestauranteSabordeCasa.Models;
using RestauranteAPI;

namespace ProyectoRestauranteSabordeCasa.Controllers
{
    [Authorize(Roles = "AD")]
    [Route("/[controller]/[action]")]
    public class ProductoController : Controller
    {
        private Productos.ProductosClient _client;
        private TipoProductos.TipoProductosClient _clientTip;

        public ProductoController()
        {
            var canal = GrpcChannel.ForAddress("https://localhost:7237");
            _client = new Productos.ProductosClient(canal);
            _clientTip = new TipoProductos.TipoProductosClient(canal);
        }

        [HttpGet]
        public async Task<IActionResult> Listado()
        {
            List<ProductoModel> lista = new List<ProductoModel>();
            var dato = await _client.ListarAsync(new EmptyProd { });
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

            return View(lista);
        }

        [HttpGet]
        public async Task<IActionResult> Agregar()
        {
            ViewBag.tipos = new SelectList(await tipoProductos(), "IdTipoProd", "NombreTipoProd");
            return View(new ProductoModel());
        }

        [HttpPost]
        public async Task<IActionResult> Agregar(ProductoModel request)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.tipos = new SelectList(await tipoProductos(), "IdTipoProd", "NombreTipoProd", request.IdTipoProd);
                return View(request);
            }
            var model = new Producto();
            model.IdTipoProd = request.IdTipoProd.ToString();
            model.NombreProd = request.NombreProd;
            model.Precio = request.Precio.ToString();
            var dato = await _client.AgregarAsync(model);
            ViewBag.mensaje = dato.Mensaje;
            ViewBag.tipos = new SelectList(await tipoProductos(), "IdTipoProd", "NombreTipoProd", model.IdTipoProd);
            return View(request);
        }

        [HttpGet("{IdProd}")]
        public async Task<IActionResult> Editar(int IdProd)
        {
            ProductoModel producto = await Buscar(IdProd);
            if (producto == null)
                return RedirectToAction("Listado");

            ViewBag.tipos = new SelectList(await tipoProductos(), "IdTipoProd", "NombreTipoProd", producto.IdTipoProd);
            return View(producto);
        }

        [HttpPost("{IdProd}")]
        public async Task<IActionResult> Editar(ProductoModel request)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.tipos = new SelectList(await tipoProductos(), "IdTipoProd", "NombreTipoProd", request.IdTipoProd);
                return View(request);
            }
            var model = new Producto();
            model.IdProd = request.IdProd.ToString();
            model.IdTipoProd = request.IdTipoProd.ToString();
            model.NombreProd = request.NombreProd;
            model.Precio = request.Precio.ToString();
            var dato = await _client.EditarAsync(model);
            ViewBag.mensaje = dato.Mensaje;
            ViewBag.tipos = new SelectList(await tipoProductos(), "IdTipoProd", "NombreTipoProd", model.IdTipoProd);
            return View(request);
        }

        [HttpGet("{IdProd}")]
        public async Task<IActionResult> Eliminar(int IdProd)
        {
            ProductoModel producto = await Buscar(IdProd);
            if (producto == null)
                return RedirectToAction("Listado");

            ViewBag.tipos = new SelectList(await tipoProductos(), "IdTipoProd", "NombreTipoProd", producto.IdTipoProd);
            return View(producto);
        }

        [HttpPost("{IdProd}")]
        public async Task<IActionResult> Eliminar(ProductoModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.tipos = new SelectList(await tipoProductos(), "IdTipoProd", "NombreTipoProd", model.IdTipoProd);
                return View(model);
            }
            var Id = new ProductoId();
            Id.Id = model.IdProd.ToString();
            var dato = await _client.EliminarAsync(Id);
            ViewBag.mensaje = dato.Mensaje;
            ViewBag.tipos = new SelectList(await tipoProductos(), "IdTipoProd", "NombreTipoProd", model.IdTipoProd);
            return View(model);
        }



        private async Task<IEnumerable<TipoProductoModel>> tipoProductos()
        {
            List<TipoProductoModel> lista = new List<TipoProductoModel>();
            var dato = await _clientTip.ListarAsync(new EmptyTip { });
            foreach (var item in dato.Item)
            {
                TipoProductoModel model = new TipoProductoModel();
                model.IdTipoProd = Convert.ToInt32(item.IdTipoProd);
                model.NombreTipoProd = item.NombreTipoProd;
                lista.Add(model);
            }
            return lista;
        }

        private async Task<ProductoModel> Buscar(int IdProd)
        {
            ProductoModel model = new ProductoModel();
            var Id = new ProductoId();
            Id.Id = IdProd.ToString();
            var dato = await _client.BuscarAsync(Id);
            model.IdProd = Convert.ToInt32(dato.IdProd);
            model.IdTipoProd = Convert.ToInt32(dato.IdTipoProd);
            model.NombreProd = dato.NombreProd;
            model.Precio = Convert.ToDecimal(dato.Precio);
            model.NombreTipoProd = dato.NombreTipoProd;

            return model;
        }
    }
}
