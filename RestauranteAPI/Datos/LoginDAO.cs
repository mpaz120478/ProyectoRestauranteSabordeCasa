using Microsoft.Data.SqlClient;
using System.Data;

namespace RestauranteAPI.Datos
{
    public class LoginDAO
    {
        #region singleton
        private static readonly LoginDAO _instancia = new LoginDAO();
        public static LoginDAO Instancia { get { return _instancia; } }
        #endregion singleton

        public UsuarioLog Iniciar(Credencial model)
        {
            SqlCommand cmd = null;
            var usuario = new UsuarioLog();

            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spValidarUsuario", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NombreUsuario", model.NombreUsuario);
                cmd.Parameters.AddWithValue("@ClaveUsuario", model.ClaveUsuario);
                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    usuario.IdUsuario = Convert.ToString(dr["IdUsuario"]);
                    usuario.IdEmp = Convert.ToString(dr["IdEmp"]);
                    usuario.NombreUsuario = Convert.ToString(dr["NombreUsuario"]);
                    usuario.RolCargo = Convert.ToString(dr["RolCargo"]);
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
            return usuario;
        }

        public Respuesta Registrar(UsuarioReg model)
        {
            SqlCommand cmd = null;
            var respuesta = new Respuesta();

            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spInsertarUsuario", cn);
                cmd.Parameters.AddWithValue("@DniEmp", model.DniEmp);
                cmd.Parameters.AddWithValue("@NombreUsuario", model.NombreUsuario);
                cmd.Parameters.AddWithValue("@ClaveUsuario", model.ClaveUsuario);
                cmd.Parameters.Add("@Registrado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                cmd.ExecuteNonQuery();

                respuesta.Registrado = cmd.Parameters["@Registrado"].Value.ToString();
                respuesta.Mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Connection.Close();
            }
            return respuesta;
        }

    }
}
