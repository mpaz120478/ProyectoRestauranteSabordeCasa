using Grpc.Core;
using RestauranteAPI.Datos;

namespace RestauranteAPI.Services
{
    public class DetallePedidoService : DetallePedidos.DetallePedidosBase
    {
        private readonly ILogger<DetallePedidoService> _logger;
        private DetallePedidoDAO _modelDao;

        public DetallePedidoService(ILogger<DetallePedidoService> logger)
        {
            _logger = logger;
            _modelDao = new DetallePedidoDAO();
        }

        public override Task<DetallePedidoss> Listar(EmptyDet request, ServerCallContext context)
        {
            DetallePedidoss lista = new DetallePedidoss();
            lista.Item.AddRange(_modelDao.Listar());
            return Task.FromResult(lista);
        }

        public override Task<DetallePedido> Buscar(DetallePedidoId request, ServerCallContext context)
        {
            DetallePedido model = new DetallePedido();
            model = _modelDao.Buscar(request);
            return Task.FromResult(model);
        }

        public override Task<RespuestaDet> Agregar(DetallePedido request, ServerCallContext context)
        {
            RespuestaDet respuesta = new RespuestaDet();
            respuesta.Mensaje = _modelDao.Agregar(request);
            return Task.FromResult(respuesta);
        }

        public override Task<RespuestaDet> Editar(DetallePedido request, ServerCallContext context)
        {
            RespuestaDet respuesta = new RespuestaDet();
            respuesta.Mensaje = _modelDao.Editar(request);
            return Task.FromResult(respuesta);
        }

        public override Task<RespuestaDet> Eliminar(DetallePedidoId request, ServerCallContext context)
        {
            RespuestaDet respuesta = new RespuestaDet();
            respuesta.Mensaje = _modelDao.Eliminar(request);
            return Task.FromResult(respuesta);
        }

        public override Task<DetallePedidoss> ListarPorPedido(DetallePedidoId request, ServerCallContext context)
        {
            DetallePedidoss lista = new DetallePedidoss();
            lista.Item.AddRange(_modelDao.ListarPorPedido(request));
            return Task.FromResult(lista);
        }
    }
}
