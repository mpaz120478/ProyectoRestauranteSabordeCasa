using System.ComponentModel.DataAnnotations;

namespace ProyectoRestauranteSabordeCasa.Models
{
    public class DetallePedidoModel
    {
        public int IdDetallePed { get; set; }
        public int IdPedido { get; set; }
        public int IdProd { get; set; }
        public int Cantidad { get; set; }
        public string? Observacion { get; set; }

        [Display(Name = "Producto")]
        public string? NombreProd { get; set; }
    }
}
