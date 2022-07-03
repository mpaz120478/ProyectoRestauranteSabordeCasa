using System.ComponentModel.DataAnnotations;

namespace ProyectoRestauranteSabordeCasa.Models
{
    public class TipoProductoModel
    {
        public int IdTipoProd { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        [Display(Name = "Tipo de Producto")]
        public string? NombreTipoProd { get; set; }
    }
}
