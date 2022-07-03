namespace ProyectoRestauranteSabordeCasa.Models
{
    public class PedidoModel
    {
        public int IdPedido { get; set; }
        public int IdUsuario { get; set; }
        public string? MesaPedido { get; set; }
        public DateTime FechaPedido { get; set; }
        public bool Pendiente { get; set; }
    }
}
