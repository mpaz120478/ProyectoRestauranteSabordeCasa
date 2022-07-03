using Microsoft.Data.SqlClient;
using System.Data;

namespace RestauranteAPI.Datos
{
    public class ProductoDAO
    {
        #region singleton
        private static readonly ProductoDAO _instancia = new ProductoDAO();
        public static ProductoDAO Instancia { get { return _instancia; } }
        #endregion singleton

        public List<Producto> Listar()
        {
            SqlCommand cmd = new SqlCommand();
            var lista = new List<Producto>();
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spProducto", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Opcion", 1);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new Producto()
                    {
                        IdProd = Convert.ToString(dr["IdProd"]),
                        IdTipoProd = Convert.ToString(dr["IdTipoProd"]),
                        NombreProd = Convert.ToString(dr["NombreProd"]),
                        Precio = Convert.ToString(dr["Precio"]),
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

        public Producto Buscar(ProductoId request)
        {
            SqlCommand cmd = new SqlCommand();
            var model = new Producto();
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spProducto", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdProd", request.Id);
                cmd.Parameters.AddWithValue("@Opcion", 2);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    model = new Producto()
                    {
                        IdProd = Convert.ToString(dr["IdProd"]),
                        IdTipoProd = Convert.ToString(dr["IdTipoProd"]),
                        NombreProd = Convert.ToString(dr["NombreProd"]),
                        Precio = Convert.ToString(dr["Precio"]),
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

        public string Agregar(Producto request)
        {
            SqlCommand cmd = new SqlCommand();
            string mensaje = "";
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spProducto", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdTipoProd", request.IdTipoProd);
                cmd.Parameters.AddWithValue("@NombreProd", request.NombreProd);
                cmd.Parameters.AddWithValue("@Precio", request.Precio);
                cmd.Parameters.AddWithValue("@Opcion", 3);
                cn.Open();
                int nro = cmd.ExecuteNonQuery();
                mensaje = $"Se ha registrado {nro} Producto";
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

        public string Editar(Producto request)
        {
            SqlCommand cmd = new SqlCommand();
            string mensaje = "";
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spProducto", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdProd", request.IdProd);
                cmd.Parameters.AddWithValue("@IdTipoProd", request.IdTipoProd);
                cmd.Parameters.AddWithValue("@NombreProd", request.NombreProd);
                cmd.Parameters.AddWithValue("@Precio", request.Precio);
                cmd.Parameters.AddWithValue("@Opcion", 4);
                cn.Open();
                int nro = cmd.ExecuteNonQuery();
                mensaje = $"Se ha actualizado {nro} Producto";
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

        public string Eliminar(ProductoId request)
        {
            SqlCommand cmd = new SqlCommand();
            string mensaje = "";
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spProducto", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdProd", request.Id);
                cmd.Parameters.AddWithValue("@Opcion", 5);
                cn.Open();
                int nro = cmd.ExecuteNonQuery();
                mensaje = $"Se ha eliminado {nro} Producto";
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
