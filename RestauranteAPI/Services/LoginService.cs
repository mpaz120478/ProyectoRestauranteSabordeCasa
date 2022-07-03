using Grpc.Core;
using RestauranteAPI.Datos;

namespace RestauranteAPI.Services
{
    public class LoginService : Login.LoginBase
    {
        private readonly ILogger<LoginService> _logger;

        public LoginService(ILogger<LoginService> logger)
        {
            _logger = logger;
        }

        public override Task<UsuarioLog> Iniciar(Credencial request, ServerCallContext context)
        {
            LoginDAO modelDao = new LoginDAO();
            UsuarioLog model = new UsuarioLog();
            var datos = modelDao.Iniciar(request);
            model.IdUsuario = datos.IdUsuario;
            model.IdEmp = datos.IdEmp;
            model.NombreUsuario = datos.NombreUsuario;
            model.RolCargo = datos.RolCargo;
            return Task.FromResult(model);
        }

        public override Task<Respuesta> Registrar(UsuarioReg request, ServerCallContext context)
        {
            LoginDAO modelDao = new LoginDAO();
            Respuesta model = new Respuesta();
            var datos = modelDao.Registrar(request);
            model.Mensaje = datos.Mensaje;
            model.Registrado = datos.Registrado;
            return Task.FromResult(model);
        }

    }
}
