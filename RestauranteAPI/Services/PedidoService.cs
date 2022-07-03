using Grpc.Core;
using RestauranteAPI.Datos;

namespace RestauranteAPI.Services
{
    public class PedidoService : Pedidos.PedidosBase
    {
        private readonly ILogger<PedidoService> _logger;
        private PedidoDAO _modelDao;

        public PedidoService(ILogger<PedidoService> logger)
        {
            _logger = logger;
            _modelDao = new PedidoDAO();
        }

        public override Task<Pedidoss> Listar(EmptyPed request, ServerCallContext context)
        {
            Pedidoss lista = new Pedidoss();
            lista.Item.AddRange(_modelDao.Listar());
            return Task.FromResult(lista);
        }

        public override Task<Pedido> Buscar(PedidoId request, ServerCallContext context)
        {
            Pedido model = new Pedido();
            model = _modelDao.Buscar(request);
            return Task.FromResult(model);
        }

        public override Task<RespuestaPed> Agregar(Pedido request, ServerCallContext context)
        {
            RespuestaPed respuesta = new RespuestaPed();
            respuesta.Mensaje = _modelDao.Agregar(request);
            return Task.FromResult(respuesta);
        }

        public override Task<RespuestaPed> Editar(Pedido request, ServerCallContext context)
        {
            RespuestaPed respuesta = new RespuestaPed();
            respuesta.Mensaje = _modelDao.Editar(request);
            return Task.FromResult(respuesta);
        }

        public override Task<RespuestaPed> Eliminar(PedidoId request, ServerCallContext context)
        {
            RespuestaPed respuesta = new RespuestaPed();
            respuesta.Mensaje = _modelDao.Eliminar(request);
            return Task.FromResult(respuesta);
        }

        public override Task<Pedidoss> ListarPorUsuario(PedidoId request, ServerCallContext context)
        {
            Pedidoss lista = new Pedidoss();
            lista.Item.AddRange(_modelDao.ListarPorUsuario(request));
            return Task.FromResult(lista);
        }
    }
}
