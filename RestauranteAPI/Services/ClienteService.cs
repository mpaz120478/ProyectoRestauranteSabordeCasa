using Grpc.Core;
using RestauranteAPI.Datos;

namespace RestauranteAPI.Services
{
    public class ClienteService : Clientes.ClientesBase
    {
        private readonly ILogger<ClienteService> _logger;
        private ClienteDAO _modelDao;

        public ClienteService(ILogger<ClienteService> logger)
        {
            _logger = logger;
            _modelDao = new ClienteDAO();
        }

        public override Task<Clientess> Listar(EmptyCli request, ServerCallContext context)
        {
            Clientess lista = new Clientess();
            lista.Item.AddRange(_modelDao.Listar());
            return Task.FromResult(lista);
        }

        public override Task<Cliente> Buscar(ClienteId request, ServerCallContext context)
        {
            Cliente model = new Cliente();
            model = _modelDao.Buscar(request);
            return Task.FromResult(model);
        }

        public override Task<RespuestaCli> Agregar(Cliente request, ServerCallContext context)
        {
            RespuestaCli respuesta = new RespuestaCli();
            respuesta.Mensaje = _modelDao.Agregar(request);
            return Task.FromResult(respuesta);
        }

        public override Task<RespuestaCli> Editar(Cliente request, ServerCallContext context)
        {
            RespuestaCli respuesta = new RespuestaCli();
            respuesta.Mensaje = _modelDao.Editar(request);
            return Task.FromResult(respuesta);
        }

        public override Task<RespuestaCli> Eliminar(ClienteId request, ServerCallContext context)
        {
            RespuestaCli respuesta = new RespuestaCli();
            respuesta.Mensaje = _modelDao.Eliminar(request);
            return Task.FromResult(respuesta);
        }
    }
}
