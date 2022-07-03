using System.ComponentModel.DataAnnotations;

namespace ProyectoRestauranteSabordeCasa.Models
{
    public class ProductoModel
    {
        public int IdProd { get; set; }

        [Display(Name = "Tipo Producto")]
        public int IdTipoProd { get; set; }

        [Display(Name = "Producto")]
        public string? NombreProd { get; set; }
        public decimal Precio { get; set; }


        [Display(Name = "Tipo Producto")]
        public string? NombreTipoProd { get; set; }

    }
}
