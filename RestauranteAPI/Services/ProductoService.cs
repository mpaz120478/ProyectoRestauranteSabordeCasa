using Grpc.Core;
using RestauranteAPI.Datos;

namespace RestauranteAPI.Services
{
    public class ProductoService : Productos.ProductosBase
    {
        private readonly ILogger<ProductoService> _logger;
        private ProductoDAO _modelDao;

        public ProductoService(ILogger<ProductoService> logger)
        {
            _logger = logger;
            _modelDao = new ProductoDAO();
        }

        public override Task<Productoss> Listar(EmptyProd request, ServerCallContext context)
        {
            Productoss lista = new Productoss();
            lista.Item.AddRange(_modelDao.Listar());
            return Task.FromResult(lista);
        }

        public override Task<Producto> Buscar(ProductoId request, ServerCallContext context)
        {
            Producto model = new Producto();
            model = _modelDao.Buscar(request);
            return Task.FromResult(model);
        }

        public override Task<RespuestaProd> Agregar(Producto request, ServerCallContext context)
        {
            RespuestaProd respuesta = new RespuestaProd();
            respuesta.Mensaje = _modelDao.Agregar(request);
            return Task.FromResult(respuesta);
        }

        public override Task<RespuestaProd> Editar(Producto request, ServerCallContext context)
        {
            RespuestaProd respuesta = new RespuestaProd();
            respuesta.Mensaje = _modelDao.Editar(request);
            return Task.FromResult(respuesta);
        }

        public override Task<RespuestaProd> Eliminar(ProductoId request, ServerCallContext context)
        {
            RespuestaProd respuesta = new RespuestaProd();
            respuesta.Mensaje = _modelDao.Eliminar(request);
            return Task.FromResult(respuesta);
        }
    }
}
