using Grpc.Core;
using RestauranteAPI.Datos;

namespace RestauranteAPI.Services
{
    public class EmpleadoService: Empleados.EmpleadosBase
    {
        private readonly ILogger<EmpleadoService> _logger;
        private EmpleadosDAO _modelDao;

        public EmpleadoService(ILogger<EmpleadoService> logger)
        {
            _logger = logger;
            _modelDao = new EmpleadosDAO();
        }

        public override Task<Empleadoss> Listar(EmptyEmp request, ServerCallContext context)
        {
            Empleadoss lista = new Empleadoss();
            lista.Item.AddRange(_modelDao.Listar());
            return Task.FromResult(lista);
        }

        public override Task<Empleado> Buscar(EmpleadoId request, ServerCallContext context)
        {
            Empleado model = new Empleado();
            model = _modelDao.Buscar(request);
            return Task.FromResult(model);
        }

        public override Task<RespuestaEmp> Agregar(Empleado request, ServerCallContext context)
        {
            RespuestaEmp respuesta = new RespuestaEmp();
            respuesta.Mensaje = _modelDao.Agregar(request);
            return Task.FromResult(respuesta);
        }

        public override Task<RespuestaEmp> Editar(Empleado request, ServerCallContext context)
        {
            RespuestaEmp respuesta = new RespuestaEmp();
            respuesta.Mensaje = _modelDao.Editar(request);
            return Task.FromResult(respuesta);
        }

        public override Task<RespuestaEmp> Eliminar(EmpleadoId request, ServerCallContext context)
        {
            RespuestaEmp respuesta = new RespuestaEmp();
            respuesta.Mensaje = _modelDao.Eliminar(request);
            return Task.FromResult(respuesta);
        }
    }
}
