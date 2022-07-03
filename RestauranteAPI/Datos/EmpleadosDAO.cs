using Microsoft.Data.SqlClient;
using System.Data;

namespace RestauranteAPI.Datos
{
    public class EmpleadosDAO
    {
        #region singleton
        private static readonly EmpleadosDAO _instancia = new EmpleadosDAO();
        public static EmpleadosDAO Instancia { get { return _instancia; } }
        #endregion singleton

        public List<Empleado> Listar()
        {
            SqlCommand cmd = new SqlCommand();
            var lista = new List<Empleado>();
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spEmpleado", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Opcion", 1);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new Empleado()
                    {
                        IdEmp = Convert.ToString(dr["IdEmp"]),
                        IdCargo = Convert.ToString(dr["IdCargo"]),
                        DniEmp = Convert.ToString(dr["DniEmp"]),
                        ApellidoEmp = Convert.ToString(dr["ApellidoEmp"]),
                        NombreEmp = Convert.ToString(dr["NombreEmp"]),
                        TelefonoEmp = Convert.ToString(dr["TelefonoEmp"]),
                        InicioContratoEmp = Convert.ToString(dr["InicioContratoEmp"]),
                        SueldoEmp = Convert.ToString(dr["SueldoEmp"]),
                        ActivoEmp = Convert.ToString(dr["ActivoEmp"]),
                        NombreCargo = Convert.ToString(dr["NombreCargo"])
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

        public Empleado Buscar(EmpleadoId request)
        {
            SqlCommand cmd = new SqlCommand();
            var model = new Empleado();
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spEmpleado", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdEmp", request.Id);
                cmd.Parameters.AddWithValue("@Opcion", 2);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    model = new Empleado()
                    {
                        IdEmp = Convert.ToString(dr["IdEmp"]),
                        IdCargo = Convert.ToString(dr["IdCargo"]),
                        DniEmp = Convert.ToString(dr["DniEmp"]),
                        ApellidoEmp = Convert.ToString(dr["ApellidoEmp"]),
                        NombreEmp = Convert.ToString(dr["NombreEmp"]),
                        TelefonoEmp = Convert.ToString(dr["TelefonoEmp"]),
                        InicioContratoEmp = Convert.ToString(dr["InicioContratoEmp"]),
                        SueldoEmp = Convert.ToString(dr["SueldoEmp"]),
                        ActivoEmp = Convert.ToString(dr["ActivoEmp"]),
                        NombreCargo = Convert.ToString(dr["NombreCargo"])
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

        public string Agregar(Empleado request)
        {
            SqlCommand cmd = new SqlCommand();
            string mensaje = "";
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spEmpleado", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DniEmp", request.DniEmp);
                cmd.Parameters.AddWithValue("@ApellidoEmp", request.ApellidoEmp);
                cmd.Parameters.AddWithValue("@NombreEmp", request.NombreEmp);
                cmd.Parameters.AddWithValue("@Telefono", request.TelefonoEmp);
                cmd.Parameters.AddWithValue("@InicioContrato", request.InicioContratoEmp);
                cmd.Parameters.AddWithValue("@SueldoEmp", request.SueldoEmp);
                cmd.Parameters.AddWithValue("@IdCargo", request.IdCargo);
                cmd.Parameters.AddWithValue("@Opcion", 3);
                cn.Open();
                int nro = cmd.ExecuteNonQuery();
                mensaje = $"Se ha registrado {nro} empleado";
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

        public string Editar(Empleado request)
        {
            SqlCommand cmd = new SqlCommand();
            string mensaje = "";
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spEmpleado", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdEmp", request.IdEmp);
                cmd.Parameters.AddWithValue("@DniEmp", request.DniEmp);
                cmd.Parameters.AddWithValue("@ApellidoEmp", request.ApellidoEmp);
                cmd.Parameters.AddWithValue("@NombreEmp", request.NombreEmp);
                cmd.Parameters.AddWithValue("@Telefono", request.TelefonoEmp);
                cmd.Parameters.AddWithValue("@InicioContrato", request.InicioContratoEmp);
                cmd.Parameters.AddWithValue("@SueldoEmp", request.SueldoEmp);
                cmd.Parameters.AddWithValue("@IdCargo", request.IdCargo);
                cmd.Parameters.AddWithValue("@Opcion", 4);
                cn.Open();
                int nro = cmd.ExecuteNonQuery();
                mensaje = $"Se ha actualizado {nro} empleado";
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

        public string Eliminar(EmpleadoId request)
        {
            SqlCommand cmd = new SqlCommand();
            string mensaje = "";
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spEmpleado", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdEmp", request.Id);
                cmd.Parameters.AddWithValue("@Opcion", 5);
                cn.Open();
                int nro = cmd.ExecuteNonQuery();
                mensaje = $"Se ha eliminado {nro} empleado";
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
