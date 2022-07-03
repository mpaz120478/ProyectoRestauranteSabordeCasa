using Microsoft.Data.SqlClient;
using System.Data;

namespace RestauranteAPI.Datos
{
    public class DetallePedidoDAO
    {
        #region singleton
        private static readonly DetallePedidoDAO _instancia = new DetallePedidoDAO();
        public static DetallePedidoDAO Instancia { get { return _instancia; } }
        #endregion singleton

        public List<DetallePedido> Listar()
        {
            SqlCommand cmd = new SqlCommand();
            var lista = new List<DetallePedido>();
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spDetallePedido", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Opcion", 1);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new DetallePedido()
                    {
                        IdDetallePed = Convert.ToString(dr["IdDetallePed"]),
                        IdPedido = Convert.ToString(dr["IdPedido"]),
                        IdProd = Convert.ToString(dr["IdProd"]),
                        Cantidad = Convert.ToString(dr["Cantidad"]),
                        Observacion = Convert.ToString(dr["Observacion"]),
                        NombreProd = Convert.ToString(dr["NombreProd"])
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

        public DetallePedido Buscar(DetallePedidoId request)
        {
            SqlCommand cmd = new SqlCommand();
            var model = new DetallePedido();
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spDetallePedido", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdDetallePed", request.Id);
                cmd.Parameters.AddWithValue("@Opcion", 2);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    model = new DetallePedido()
                    {
                        IdDetallePed = Convert.ToString(dr["IdDetallePed"]),
                        IdPedido = Convert.ToString(dr["IdPedido"]),
                        IdProd = Convert.ToString(dr["IdProd"]),
                        Cantidad = Convert.ToString(dr["Cantidad"]),
                        Observacion = Convert.ToString(dr["Observacion"]),
                        NombreProd = Convert.ToString(dr["NombreProd"])
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

        public string Agregar(DetallePedido request)
        {
            SqlCommand cmd = new SqlCommand();
            string mensaje = "";
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spDetallePedido", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdPedido", request.IdPedido);
                cmd.Parameters.AddWithValue("@IdProd", request.IdProd);
                cmd.Parameters.AddWithValue("@Cantidad", request.Cantidad);
                cmd.Parameters.AddWithValue("@Observacion", request.Observacion);
                cmd.Parameters.AddWithValue("@Opcion", 3);
                cn.Open();
                int nro = cmd.ExecuteNonQuery();
                mensaje = $"Se ha registrado {nro} DetallePedido";
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

        public string Editar(DetallePedido request)
        {
            SqlCommand cmd = new SqlCommand();
            string mensaje = "";
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spDetallePedido", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdDetallePed", request.IdDetallePed);
                cmd.Parameters.AddWithValue("@IdPedido", request.IdPedido);
                cmd.Parameters.AddWithValue("@IdProd", request.IdProd);
                cmd.Parameters.AddWithValue("@Cantidad", request.Cantidad);
                cmd.Parameters.AddWithValue("@Observacion", request.Observacion);
                cmd.Parameters.AddWithValue("@Opcion", 4);
                cn.Open();
                int nro = cmd.ExecuteNonQuery();
                mensaje = $"Se ha actualizado {nro} DetallePedido";
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

        public string Eliminar(DetallePedidoId request)
        {
            SqlCommand cmd = new SqlCommand();
            string mensaje = "";
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spDetallePedido", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdDetallePed", request.Id);
                cmd.Parameters.AddWithValue("@Opcion", 5);
                cn.Open();
                int nro = cmd.ExecuteNonQuery();
                mensaje = $"Se ha eliminado {nro} DetallePedido";
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

        public List<DetallePedido> ListarPorPedido(DetallePedidoId request)
        {
            SqlCommand cmd = new SqlCommand();
            var lista = new List<DetallePedido>();
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spDetallePedido", cn);
                cmd.Parameters.AddWithValue("@IdPedido", request.Id);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Opcion", 6);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new DetallePedido()
                    {
                        IdDetallePed = Convert.ToString(dr["IdDetallePed"]),
                        IdPedido = Convert.ToString(dr["IdPedido"]),
                        IdProd = Convert.ToString(dr["IdProd"]),
                        Cantidad = Convert.ToString(dr["Cantidad"]),
                        Observacion = Convert.ToString(dr["Observacion"]),
                        NombreProd = Convert.ToString(dr["NombreProd"])
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
