using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ProyectoRestauranteSabordeCasa.Models;
using System.Data;
using System.Text;
using System.Security.Cryptography;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using RestauranteAPI;
using Grpc.Net.Client;

namespace ProyectoRestauranteSabordeCasa.Controllers
{
    public class LoginController : Controller
    {
        private Login.LoginClient _client;

        public LoginController()
        {
            var canal = GrpcChannel.ForAddress("https://localhost:7237");
            _client = new Login.LoginClient(canal);
        }

        [HttpGet]
        public IActionResult Inicio()
        {
            if(User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(new UsuarioModel());
        }

        [HttpGet]
        public IActionResult Registrar()
        {
            return View(new UsuarioModel());
        }

        [HttpGet]
        [Authorize]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return Redirect("/Login");
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(UsuarioModel modelRequest)
        {

            bool registrado;
            string mensaje;

            if (modelRequest.ClaveUsuario == modelRequest.ConfirmarClave)
            {

                modelRequest.ClaveUsuario = ConvertirSha256(modelRequest.ClaveUsuario);
            }
            else
            {
                ViewData["@Mensaje"] = "Las contraseñas no coinciden";

                return View();
            }

            try
            {
                var model = new UsuarioReg();
                model.DniEmp = modelRequest.Dni;
                model.NombreUsuario = modelRequest.NombreUsuario;
                model.ClaveUsuario = modelRequest.ClaveUsuario;

                var dato = await _client.RegistrarAsync(model);

                registrado = Convert.ToBoolean(dato.Registrado);
                mensaje = dato.Mensaje;
                ViewData["@Mensaje"] = mensaje;

                if (registrado)
                {
                    return RedirectToAction("Inicio", "Login");
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                ViewData["@Mensaje"] = "Trabajador no existe.";
                return View();
            }

        }


        [HttpPost]
        public async Task<IActionResult> Inicio(UsuarioModel oUsuario)
        {
            oUsuario.ClaveUsuario = ConvertirSha256(oUsuario.ClaveUsuario);

                if (string.IsNullOrEmpty(oUsuario.NombreUsuario) || string.IsNullOrEmpty(oUsuario.ClaveUsuario))
                {
                    ModelState.AddModelError("", "Ingresar los datos seleccionados");
                }
                else
                {
                var model = new Credencial();
                model.NombreUsuario = oUsuario.NombreUsuario;
                model.ClaveUsuario = oUsuario.ClaveUsuario;

                var dato = await _client.IniciarAsync(model);

                if (dato != null)
                    {
                        if(dato.IdUsuario != "0")
                        {
                            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, dato.IdUsuario));
                            identity.AddClaim(new Claim("IdEmp", dato.IdEmp));
                            identity.AddClaim(new Claim(ClaimTypes.Name, dato.NombreUsuario));
                            identity.AddClaim(new Claim(ClaimTypes.Role, dato.RolCargo));
                            identity.AddClaim(new Claim("Rol", dato.RolCargo));
                            identity.AddClaim(new Claim("IdUser", dato.IdUsuario));

                            var principal = new ClaimsPrincipal(identity);

                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
                                new AuthenticationProperties { ExpiresUtc = DateTime.Now.AddHours(1), IsPersistent = true });

                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            //ModelState.AddModelError("", "Contraseña incorrecta");
                            ViewData["@Mensaje"] = "Contraseña incorrecta";
                        }
                    }
                    else
                    {
                        //ModelState.AddModelError("", "Usuario no registrado, REGÍSTRESE");
                        ViewData["@Mensaje"] = "Usuario no registrado, REGÍSTRESE";
                    }
                }
            
            return View(oUsuario);
        }

        public static string ConvertirSha256(string texto)
        {
            //using System.Text;
            //USAR LA REFERENCIA DE "System.Security.Cryptography"

            StringBuilder Sb = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));

                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }
    }
}
