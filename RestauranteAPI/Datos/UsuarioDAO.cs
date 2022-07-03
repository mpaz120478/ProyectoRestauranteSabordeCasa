using Microsoft.Data.SqlClient;
using System.Data;

namespace RestauranteAPI.Datos
{
    public class UsuarioDAO
    {
        #region singleton
        private static readonly UsuarioDAO _instancia = new UsuarioDAO();
        public static UsuarioDAO Instancia { get { return _instancia; } }
        #endregion singleton

        public List<Usuario> Listar()
        {
            SqlCommand cmd = new SqlCommand();
            var lista = new List<Usuario>();
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spListarUsuario", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new Usuario()
                    {
                        IdUsuario = Convert.ToString(dr["IdUsuario"]),
                        IdEmp = Convert.ToString(dr["IdEmp"]),
                        NombreUsuario = Convert.ToString(dr["NombreUsuario"]),
                        RolCargo = Convert.ToString(dr["RolCargo"]),
                        Dni = Convert.ToString(dr["DniEmp"])
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
