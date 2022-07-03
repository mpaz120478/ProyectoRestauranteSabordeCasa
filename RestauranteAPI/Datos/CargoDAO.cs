using Microsoft.Data.SqlClient;
using System.Data;

namespace RestauranteAPI.Datos
{
    public class CargoDAO
    {
        #region singleton
        private static readonly CargoDAO _instancia = new CargoDAO();
        public static CargoDAO Instancia { get { return _instancia; } }
        #endregion singleton

        public List<Cargo> Listar()
        {
            SqlCommand cmd = new SqlCommand();
            var lista = new List<Cargo>();
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spCargo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Opcion", 1);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new Cargo()
                    {
                        IdCargo = Convert.ToString(dr["IdCargo"]),
                        NombreCargo = Convert.ToString(dr["NombreCargo"]),
                        RolCargo = Convert.ToString(dr["RolCargo"])
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

        public Cargo Buscar(CargoId request)
        {
            SqlCommand cmd = new SqlCommand();
            var model = new Cargo();
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spCargo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdCargo", request.Id);
                cmd.Parameters.AddWithValue("@Opcion", 2);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    model = new Cargo()
                    {
                        IdCargo = Convert.ToString(dr["IdCargo"]),
                        NombreCargo = Convert.ToString(dr["NombreCargo"]),
                        RolCargo = Convert.ToString(dr["RolCargo"])
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

        public string Agregar(Cargo request)
        {
            SqlCommand cmd = new SqlCommand();
            string mensaje = "";
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spCargo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NombreCargo", request.NombreCargo);
                cmd.Parameters.AddWithValue("@RolCargo", request.RolCargo);
                cmd.Parameters.AddWithValue("@Opcion", 3);
                cn.Open();
                int nro = cmd.ExecuteNonQuery();
                mensaje = $"Se ha registrado {nro} cargo";
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

        public string Editar(Cargo request)
        {
            SqlCommand cmd = new SqlCommand();
            string mensaje = "";
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spCargo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdCargo", request.IdCargo);
                cmd.Parameters.AddWithValue("@NombreCargo", request.NombreCargo);
                cmd.Parameters.AddWithValue("@RolCargo", request.RolCargo);
                cmd.Parameters.AddWithValue("@Opcion", 4);
                cn.Open();
                int nro = cmd.ExecuteNonQuery();
                mensaje = $"Se ha actualizado {nro} cargo";
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

        public string Eliminar(CargoId request)
        {
            SqlCommand cmd = new SqlCommand();
            string mensaje = "";
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spCargo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdCargo", request.Id);
                cmd.Parameters.AddWithValue("@Opcion", 5);
                cn.Open();
                int nro = cmd.ExecuteNonQuery();
                mensaje = $"Se ha eliminado {nro} cargo";
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
