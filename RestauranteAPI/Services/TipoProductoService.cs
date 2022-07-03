using Grpc.Core;
using RestauranteAPI.Datos;

namespace RestauranteAPI.Services
{
    public class TipoProductoService : TipoProductos.TipoProductosBase
    {
        private readonly ILogger<TipoProductoService> _logger;
        private TipoProductoDAO _modelDao;

        public TipoProductoService(ILogger<TipoProductoService> logger)
        {
            _logger = logger;
            _modelDao = new TipoProductoDAO();
        }

        public override Task<TipoProductoss> Listar(EmptyTip request, ServerCallContext context)
        {
            TipoProductoss lista = new TipoProductoss();
            lista.Item.AddRange(_modelDao.Listar());
            return Task.FromResult(lista);
        }

        public override Task<TipoProducto> Buscar(TipoProductoId request, ServerCallContext context)
        {
            TipoProducto model = new TipoProducto();
            model = _modelDao.Buscar(request);
            return Task.FromResult(model);
        }

        public override Task<RespuestaTip> Agregar(TipoProducto request, ServerCallContext context)
        {
            RespuestaTip respuesta = new RespuestaTip();
            respuesta.Mensaje = _modelDao.Agregar(request);
            return Task.FromResult(respuesta);
        }

        public override Task<RespuestaTip> Editar(TipoProducto request, ServerCallContext context)
        {
            RespuestaTip respuesta = new RespuestaTip();
            respuesta.Mensaje = _modelDao.Editar(request);
            return Task.FromResult(respuesta);
        }

        public override Task<RespuestaTip> Eliminar(TipoProductoId request, ServerCallContext context)
        {
            RespuestaTip respuesta = new RespuestaTip();
            respuesta.Mensaje = _modelDao.Eliminar(request);
            return Task.FromResult(respuesta);
        }
    }
}
