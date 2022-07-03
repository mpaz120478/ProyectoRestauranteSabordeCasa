using System.ComponentModel.DataAnnotations;

namespace ProyectoRestauranteSabordeCasa.Models
{
    public class UsuarioModel
    {
        public int IdUsuario { get; set; }
        public int? IdEmp { get; set; }
        [Display(Name = "Nombre de Usuario")][Required(ErrorMessage = "Campo obligatorio")][StringLength(20)] public string? NombreUsuario { get; set; }
        [Required(ErrorMessage = "Campo obligatorio")][StringLength(20)] public string? ClaveUsuario { get; set; }

        [Display(Name = "DNI Empleado")][Required(ErrorMessage = "Campo obligatorio")][StringLength(8)] public string? Dni { get; set; }
        [Required(ErrorMessage = "Campo obligatorio")][StringLength(20)] public string? ConfirmarClave { get; set; }
        [Display(Name = "Rol")] public string? RolCargo { get; set; }
    }
}