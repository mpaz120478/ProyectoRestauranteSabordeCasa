namespace ProyectoRestauranteSabordeCasa.Models
{
    public class VentaModel
    {
        public int IdVenta { get; set; }
        public int IdCliente { get; set; }
        public int IdPedido { get; set; }
        public int IdUsuario { get; set; }
        public decimal PrecioBase { get; set; }
        public decimal PrecioIgv { get; set; }
        public decimal PrecioTotal { get; set; }

        public string? DniCliente { get; set; }
        public string? DatosCliente { get; set; }
    }
}
