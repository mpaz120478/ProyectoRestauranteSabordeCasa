using Grpc.Net.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoRestauranteSabordeCasa.Models;
using RestauranteAPI;

namespace ProyectoRestauranteSabordeCasa.Controllers
{
    [Authorize(Roles = "AD")]
    [Route("/[controller]/[action]")]
    public class UsuarioController : Controller
    {
        private Usuarios.UsuariosClient _client;

        public UsuarioController()
        {
            var canal = GrpcChannel.ForAddress("https://localhost:7237");
            _client = new Usuarios.UsuariosClient(canal);
        }

        // GET: UsuarioController
        public async Task<ActionResult> Listado()
        {
            List<UsuarioModel> lista = new List<UsuarioModel>();
            var dato = await _client.ListarAsync(new EmptyUsu { });
            foreach (var item in dato.Item)
            {
                UsuarioModel model = new UsuarioModel();
                model.IdUsuario = Convert.ToInt32(item.IdUsuario);
                model.IdEmp = Convert.ToInt32(item.IdEmp);
                model.NombreUsuario = item.NombreUsuario;
                model.RolCargo = item.RolCargo;
                model.Dni = item.Dni;
                lista.Add(model);
            }
            return View(lista);
        }
    }
}
