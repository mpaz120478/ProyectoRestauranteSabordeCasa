using Grpc.Core;
using RestauranteAPI.Datos;

namespace RestauranteAPI.Services
{
    public class CargoService : Cargos.CargosBase
    {
        private readonly ILogger<CargoService> _logger;
        private CargoDAO _modelDao;

        public CargoService(ILogger<CargoService> logger)
        {
            _logger = logger;
            _modelDao = new CargoDAO();
        }

        public override Task<Cargoss> Listar(EmptyCar request, ServerCallContext context)
        {
            Cargoss lista = new Cargoss();
            lista.Item.AddRange(_modelDao.Listar());
            return Task.FromResult(lista);
        }

        public override Task<Cargo> Buscar(CargoId request, ServerCallContext context)
        {
            Cargo model = new Cargo();
            model = _modelDao.Buscar(request);
            return Task.FromResult(model);
        }

        public override Task<RespuestaCar> Agregar(Cargo request, ServerCallContext context)
        {
            RespuestaCar respuesta = new RespuestaCar();
            respuesta.Mensaje = _modelDao.Agregar(request);
            return Task.FromResult(respuesta);
        }

        public override Task<RespuestaCar> Editar(Cargo request, ServerCallContext context)
        {
            RespuestaCar respuesta = new RespuestaCar();
            respuesta.Mensaje = _modelDao.Editar(request);
            return Task.FromResult(respuesta);
        }

        public override Task<RespuestaCar> Eliminar(CargoId request, ServerCallContext context)
        {
            RespuestaCar respuesta = new RespuestaCar();
            respuesta.Mensaje = _modelDao.Eliminar(request);
            return Task.FromResult(respuesta);
        }
    }
}
