using Microsoft.Data.SqlClient;
using System.Data;

namespace RestauranteAPI.Datos
{
    public class TipoProductoDAO
    {
        #region singleton
        private static readonly TipoProductoDAO _instancia = new TipoProductoDAO();
        public static TipoProductoDAO Instancia { get { return _instancia; } }
        #endregion singleton

        public List<TipoProducto> Listar()
        {
            SqlCommand cmd = new SqlCommand();
            var lista = new List<TipoProducto>();
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spTipoProducto", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Opcion", 1);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new TipoProducto()
                    {
                        IdTipoProd = Convert.ToString(dr["IdTipoProd"]),
                        NombreTipoProd = Convert.ToString(dr["NombreTipoProd"])
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

        public TipoProducto Buscar(TipoProductoId request)
        {
            SqlCommand cmd = new SqlCommand();
            var model = new TipoProducto();
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spTipoProducto", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdTipoProd", request.Id);
                cmd.Parameters.AddWithValue("@Opcion", 2);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    model = new TipoProducto()
                    {
                        IdTipoProd = Convert.ToString(dr["IdTipoProd"]),
                        NombreTipoProd = Convert.ToString(dr["NombreTipoProd"])
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

        public string Agregar(TipoProducto request)
        {
            SqlCommand cmd = new SqlCommand();
            string mensaje = "";
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spTipoProducto", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NombreTipoProd", request.NombreTipoProd);
                cmd.Parameters.AddWithValue("@Opcion", 3);
                cn.Open();
                int nro = cmd.ExecuteNonQuery();
                mensaje = $"Se ha registrado {nro} TipoProducto";
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

        public string Editar(TipoProducto request)
        {
            SqlCommand cmd = new SqlCommand();
            string mensaje = "";
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spTipoProducto", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdTipoProd", request.IdTipoProd);
                cmd.Parameters.AddWithValue("@NombreTipoProd", request.NombreTipoProd);
                cmd.Parameters.AddWithValue("@Opcion", 4);
                cn.Open();
                int nro = cmd.ExecuteNonQuery();
                mensaje = $"Se ha actualizado {nro} TipoProducto";
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

        public string Eliminar(TipoProductoId request)
        {
            SqlCommand cmd = new SqlCommand();
            string mensaje = "";
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spTipoProducto", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdTipoProd", request.Id);
                cmd.Parameters.AddWithValue("@Opcion", 5);
                cn.Open();
                int nro = cmd.ExecuteNonQuery();
                mensaje = $"Se ha eliminado {nro} TipoProducto";
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