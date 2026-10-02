using MySql.Data.MySqlClient;
namespace FlexSpace.DAL
{
    public class FlexSpaceDAL
    {
        private string _conexionString =
        "Server=127.0.0.1;Port=3306;Database=FlexSpace;Uid=root;Pwd=;";


        public (int ID, string Nombre, string Email, bool VIP, int Sanciones_Activas)? BuscarPorID0(int ID)
        {
            string query = "SELECT ID, Nombre, Email, VIP, Sanciones_Activas FROM cliente WHERE ID = @ID";
            using (MySqlConnection conexion = new MySqlConnection(_conexionString))
            {
                MySqlCommand comando = new MySqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@ID", ID);
                conexion.Open();
                using (MySqlDataReader reader = comando.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int idDb = reader.GetInt32("ID");
                        string nombreDb = reader["Nombre"].ToString();
                        string emailDb = reader["Autor"].ToString();
                        bool vipDb = reader.GetBoolean("Disponible");
                        int sanciones_activasDb = reader.GetInt32("Sanciones_Activas");

                        return (idDb, nombreDb, emailDb, vipDb, sanciones_activasDb);
                    }
                }
            }
            return null;
        }
        public (int ID, long Codigo, string TipoPuesto, decimal TBPH)? BuscarPorID1(int ID)
        {
            string query = "SELECT ID, Codigo, TipoPuesto, TBPH FROM puesto WHERE ID = @ID";
            using (MySqlConnection conexion = new MySqlConnection(_conexionString))
            {
                MySqlCommand comando = new MySqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@ID", ID);
                conexion.Open();
                using (MySqlDataReader reader = comando.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int idDb = reader.GetInt32("ID");
                        long codigoDb = reader.GetInt64("Codigo");
                        string tipopuestoDb = reader["TipoPuesto"].ToString();
                        decimal tbphDb = reader.GetInt32("TBPH");

                        return (idDb, codigoDb, tipopuestoDb, tbphDb);
                    }
                }
            }
            return null;
        }
        public (int ID, int ClienteID, int PuestoID, DateTime Fecha_Inicio, DateTime Fecha_Fin, string Estado, decimal CostoTotal)? BuscarPorID2(int ID)
        {
            string query = "SELECT ID, ClienteID, PuestoID, Fecha_Inicio, Fecha_Fin, Estado, CostoTotal FROM reserva WHERE ID = @ID";
            using (MySqlConnection conexion = new MySqlConnection(_conexionString))
            {
                MySqlCommand comando = new MySqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@ID", ID);
                conexion.Open();
                using (MySqlDataReader reader = comando.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int idDb = reader.GetInt32("ID");
                        int clienteidDb = reader.GetInt32("ID");
                        int puestoidDb = reader.GetInt32("PuestoID");
                        DateTime fecha_inicioDb = reader.GetDateTime("Fecha_Inicio");
                        DateTime fecha_finDb = reader.GetDateTime("Fecha_Fin");
                        string estadoDb = reader["Estado"].ToString();
                        decimal costototalDb = reader.GetInt32("CostoTotal");

                        return (idDb, clienteidDb, puestoidDb, fecha_inicioDb, fecha_finDb, estadoDb, costototalDb);
                    }
                }
            }
            return null;
        }
    }
}