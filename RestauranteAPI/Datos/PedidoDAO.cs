using Microsoft.Data.SqlClient;
using System.Data;

namespace RestauranteAPI.Datos
{
    public class PedidoDAO
    {
        #region singleton
        private static readonly PedidoDAO _instancia = new PedidoDAO();
        public static PedidoDAO Instancia { get { return _instancia; } }
        #endregion singleton

        public List<Pedido> Listar()
        {
            SqlCommand cmd = new SqlCommand();
            var lista = new List<Pedido>();
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spPedido", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Opcion", 1);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new Pedido()
                    {
                        IdPedido = Convert.ToString(dr["IdPedido"]),
                        IdUsuario = Convert.ToString(dr["IdUsuario"]),
                        MesaPedido = Convert.ToString(dr["MesaPedido"]),
                        FechaPedido = Convert.ToString(dr["FechaPedido"]),
                        Pendiente = Convert.ToString(dr["Pendiente"])
                    });
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            finally
            {
                cmd.Connection.Close();
            }
            return lista;
        }

        public Pedido Buscar(PedidoId request)
        {
            SqlCommand cmd = new SqlCommand();
            var model = new Pedido();
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spPedido", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdPedido", request.Id);
                cmd.Parameters.AddWithValue("@Opcion", 2);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    model = new Pedido()
                    {
                        IdPedido = Convert.ToString(dr["IdPedido"]),
                        IdUsuario = Convert.ToString(dr["IdUsuario"]),
                        MesaPedido = Convert.ToString(dr["MesaPedido"]),
                        FechaPedido = Convert.ToString(dr["FechaPedido"]),
                        Pendiente = Convert.ToString(dr["Pendiente"])
                    };
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            finally
            {
                cmd.Connection.Close();
            }
            return model;
        }

        public string Agregar(Pedido request)
        {
            SqlCommand cmd = new SqlCommand();
            string mensaje = "";
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spPedido", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdUsuario", request.IdUsuario);
                cmd.Parameters.AddWithValue("@MesaPedido", request.MesaPedido);
                cmd.Parameters.AddWithValue("@FechaPedido", request.FechaPedido);
                cmd.Parameters.AddWithValue("@Opcion", 3);
                cn.Open();
                int nro = cmd.ExecuteNonQuery();
                mensaje = $"Se ha registrado {nro} Pedido";
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            finally
            {
                cmd.Connection.Close();
            }
            return mensaje;
        }

        public string Editar(Pedido request)
        {
            SqlCommand cmd = new SqlCommand();
            string mensaje = "";
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spPedido", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdPedido", request.IdPedido);
                cmd.Parameters.AddWithValue("@IdUsuario", request.IdUsuario);
                cmd.Parameters.AddWithValue("@MesaPedido", request.MesaPedido);
                cmd.Parameters.AddWithValue("@FechaPedido", request.FechaPedido);
                cmd.Parameters.AddWithValue("@Opcion", 4);
                cn.Open();
                int nro = cmd.ExecuteNonQuery();
                mensaje = $"Se ha actualizado {nro} Pedido";
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            finally
            {
                cmd.Connection.Close();
            }
            return mensaje;
        }

        public string Eliminar(PedidoId request)
        {
            SqlCommand cmd = new SqlCommand();
            string mensaje = "";
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spPedido", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdPedido", request.Id);
                cmd.Parameters.AddWithValue("@Opcion", 5);
                cn.Open();
                int nro = cmd.ExecuteNonQuery();
                mensaje = $"Se ha eliminado {nro} Pedido";
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            finally
            {
                cmd.Connection.Close();
            }
            return mensaje;
        }

        public List<Pedido> ListarPorUsuario(PedidoId request)
        {
            SqlCommand cmd = new SqlCommand();
            var lista = new List<Pedido>();
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spPedido", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdUsuario", request.Id);
                cmd.Parameters.AddWithValue("@Opcion", 6);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new Pedido()
                    {
                        IdPedido = Convert.ToString(dr["IdPedido"]),
                        IdUsuario = Convert.ToString(dr["IdUsuario"]),
                        MesaPedido = Convert.ToString(dr["MesaPedido"]),
                        FechaPedido = Convert.ToString(dr["FechaPedido"]),
                        Pendiente = Convert.ToString(dr["Pendiente"])
                    });
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            finally
            {
                cmd.Connection.Close();
            }
            return lista;
        }
    }
}
