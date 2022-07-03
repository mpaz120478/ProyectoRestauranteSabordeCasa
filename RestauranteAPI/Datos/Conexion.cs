using Microsoft.Data.SqlClient;

namespace RestauranteAPI.Datos
{
    public class Conexion
    {
        #region singleton
        private static readonly Conexion _instancia = new Conexion();
        public static Conexion Instancia { get { return _instancia; } }
        #endregion singleton

        public SqlConnection conectar()
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "";
            return cn;
        }
    }
}
