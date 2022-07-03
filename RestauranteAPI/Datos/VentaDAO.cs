using Microsoft.Data.SqlClient;
using System.Data;

namespace RestauranteAPI.Datos
{
    internal class VentaDAO
    {
        #region singleton
        private static readonly VentaDAO _instancia = new VentaDAO();
        public static VentaDAO Instancia { get { return _instancia; } }
        #endregion singleton

        public List<Venta> Listar()
        {
            SqlCommand cmd = new SqlCommand();
            var lista = new List<Venta>();
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spVenta", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Opcion", 1);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new Venta()
                    {
                        IdVenta = Convert.ToString(dr["IdVenta"]),
                        IdPedido = Convert.ToString(dr["IdPedido"]),
                        IdCliente = Convert.ToString(dr["IdCliente"]),
                        IdUsuario = Convert.ToString(dr["IdUsuario"]),
                        PrecioIgv = Convert.ToString(dr["PrecioIgv"]),
                        PrecioBase = Convert.ToString(dr["PrecioBase"]),
                        PrecioTotal = Convert.ToString(dr["PrecioTotal"]),
                        DniCliente = Convert.ToString(dr["DniCliente"]),
                        DatosCliente = Convert.ToString(dr["DatosCliente"])
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

        public Venta Buscar(VentaId request)
        {
            SqlCommand cmd = new SqlCommand();
            var model = new Venta();
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spVenta", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdVenta", request.Id);
                cmd.Parameters.AddWithValue("@Opcion", 2);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    model = new Venta()
                    {
                        IdVenta = Convert.ToString(dr["IdVenta"]),
                        IdPedido = Convert.ToString(dr["IdPedido"]),
                        IdCliente = Convert.ToString(dr["IdCliente"]),
                        IdUsuario = Convert.ToString(dr["IdUsuario"]),
                        PrecioIgv = Convert.ToString(dr["PrecioIgv"]),
                        PrecioBase = Convert.ToString(dr["PrecioBase"]),
                        PrecioTotal = Convert.ToString(dr["PrecioTotal"]),
                        DniCliente = Convert.ToString(dr["DniCliente"]),
                        DatosCliente = Convert.ToString(dr["DatosCliente"])
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

        public string Agregar(Venta request)
        {
            SqlCommand cmd = new SqlCommand();
            string mensaje = "";
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spVenta", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdPedido", request.IdPedido);
                cmd.Parameters.AddWithValue("@IdUsuario", request.IdUsuario);
                cmd.Parameters.AddWithValue("@PrecioIgv", request.PrecioIgv);
                cmd.Parameters.AddWithValue("@PrecioBase", request.PrecioBase);
                cmd.Parameters.AddWithValue("@PrecioTotal", request.PrecioTotal);
                cmd.Parameters.AddWithValue("@DniCliente", request.DniCliente);
                cmd.Parameters.AddWithValue("@DatosCliente", request.DatosCliente);
                cmd.Parameters.AddWithValue("@Opcion", 3);
                cn.Open();
                int nro = cmd.ExecuteNonQuery();
                mensaje = $"Se ha registrado {nro} Venta";
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

        public string Editar(Venta request)
        {
            SqlCommand cmd = new SqlCommand();
            string mensaje = "";
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spVenta", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdVenta", request.IdVenta);
                cmd.Parameters.AddWithValue("@IdPedido", request.IdPedido);
                cmd.Parameters.AddWithValue("@IdUsuario", request.IdUsuario);
                cmd.Parameters.AddWithValue("@PrecioIgv", request.PrecioIgv);
                cmd.Parameters.AddWithValue("@PrecioBase", request.PrecioBase);
                cmd.Parameters.AddWithValue("@PrecioTotal", request.PrecioTotal);
                cmd.Parameters.AddWithValue("@DniCliente", request.DniCliente);
                cmd.Parameters.AddWithValue("@DatosCliente", request.DatosCliente);
                cmd.Parameters.AddWithValue("@Opcion", 4);
                cn.Open();
                int nro = cmd.ExecuteNonQuery();
                mensaje = $"Se ha actualizado {nro} Venta";
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

        public string Eliminar(VentaId request)
        {
            SqlCommand cmd = new SqlCommand();
            string mensaje = "";
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spVenta", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdVenta", request.Id);
                cmd.Parameters.AddWithValue("@Opcion", 5);
                cn.Open();
                int nro = cmd.ExecuteNonQuery();
                mensaje = $"Se ha eliminado {nro} Venta";
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
    }
}