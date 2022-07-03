using Grpc.Core;
using RestauranteAPI.Datos;

namespace RestauranteAPI.Services
{
    public class VentaService : Ventas.VentasBase
    {
        private readonly ILogger<VentaService> _logger;
        private VentaDAO _modelDao;

        public VentaService(ILogger<VentaService> logger)
        {
            _logger = logger;
            _modelDao = new VentaDAO();
        }

        public override Task<Ventass> Listar(EmptyVen request, ServerCallContext context)
        {
            Ventass lista = new Ventass();
            lista.Item.AddRange(_modelDao.Listar());
            return Task.FromResult(lista);
        }

        public override Task<Venta> Buscar(VentaId request, ServerCallContext context)
        {
            Venta model = new Venta();
            model = _modelDao.Buscar(request);
            return Task.FromResult(model);
        }

        public override Task<RespuestaVen> Agregar(Venta request, ServerCallContext context)
        {
            RespuestaVen respuesta = new RespuestaVen();
            respuesta.Mensaje = _modelDao.Agregar(request);
            return Task.FromResult(respuesta);
        }

        public override Task<RespuestaVen> Editar(Venta request, ServerCallContext context)
        {
            RespuestaVen respuesta = new RespuestaVen();
            respuesta.Mensaje = _modelDao.Editar(request);
            return Task.FromResult(respuesta);
        }

        public override Task<RespuestaVen> Eliminar(VentaId request, ServerCallContext context)
        {
            RespuestaVen respuesta = new RespuestaVen();
            respuesta.Mensaje = _modelDao.Eliminar(request);
            return Task.FromResult(respuesta);
        }
    }
}
