using Grpc.Core;
using RestauranteAPI.Datos;

namespace RestauranteAPI.Services
{
    public class UsuarioService : Usuarios.UsuariosBase
    {
        private readonly ILogger<UsuarioService> _logger;
        private UsuarioDAO _modelDao;

        public UsuarioService(ILogger<UsuarioService> logger)
        {
            _logger = logger;
            _modelDao = new UsuarioDAO();
        }

        public override Task<Usuarioss> Listar(EmptyUsu request, ServerCallContext context)
        {
            Usuarioss lista = new Usuarioss();
            lista.Item.AddRange(_modelDao.Listar());
            return Task.FromResult(lista);
        }

        public override Task<Usuario> Buscar(UsuarioId request, ServerCallContext context)
        {
            return base.Buscar(request, context);
        }

        public override Task<RespuestaUsu> Agregar(Usuario request, ServerCallContext context)
        {
            return base.Agregar(request, context);
        }

        public override Task<RespuestaUsu> Editar(Usuario request, ServerCallContext context)
        {
            return base.Editar(request, context);
        }

        public override Task<RespuestaUsu> Eliminar(UsuarioId request, ServerCallContext context)
        {
            return base.Eliminar(request, context);
        }
    }
}
