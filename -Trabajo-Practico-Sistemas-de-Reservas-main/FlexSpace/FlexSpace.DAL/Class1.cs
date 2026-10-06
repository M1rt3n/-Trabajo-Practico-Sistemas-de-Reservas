using MySql.Data.MySqlClient;

namespace FlexSpace.DAL
{
    public class FlexSpaceDAL
    {
        private string _conexionString =
        "Server=127.0.0.1;Port=3307;Database=FlexSpace;Uid=root;Pwd=;";
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
                        string emailDb = reader["Email"].ToString();
                        bool vipDb = reader.GetBoolean("VIP");
                        int sanciones_activasDb = reader.GetInt32("Sanciones_Activas");

                        return (idDb, nombreDb, emailDb, vipDb, sanciones_activasDb);
                    }
                }
            }

            return null;
        }
        public List<(int ID, string Nombre, string Email, bool VIP, int Sanciones_Activas)> BuscarPorSanciones()
        {
            string query = "SELECT ID, Nombre, Email, VIP, Sanciones_Activas FROM cliente WHERE Sanciones_Activas > 0";

            List<(int ID, string Nombre, string Email, bool VIP, int Sanciones_Activas)> clientes = new();

            using (MySqlConnection conexion = new MySqlConnection(_conexionString))
            {
                MySqlCommand comando = new MySqlCommand(query, conexion);

                conexion.Open();

                using (MySqlDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int idDb = reader.GetInt32("ID");
                        string nombreDb = reader["Nombre"].ToString();
                        string emailDb = reader["Email"].ToString();
                        bool vipDb = reader.GetBoolean("VIP");
                        int sanciones_activasDb = reader.GetInt32("Sanciones_Activas");

                        clientes.Add((idDb, nombreDb, emailDb, vipDb, sanciones_activasDb));
                    }
                }
            }

            return clientes;
        }
        public bool IncrementarSancion(int ID)
        {
            string query = "UPDATE cliente SET Sanciones_Activas = Sanciones_Activas + 1 WHERE ID = @ID";

            using (MySqlConnection conexion = new MySqlConnection(_conexionString))
            {
                MySqlCommand comando = new MySqlCommand(query, conexion);

                comando.Parameters.AddWithValue("@ID", ID);

                conexion.Open();

                int FilasAfectadas = comando.ExecuteNonQuery();

                return FilasAfectadas > 0;
            }
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
                        decimal tbphDb = reader.GetDecimal("TBPH");

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
                        int clienteidDb = reader.GetInt32("ClienteID");
                        int puestoidDb = reader.GetInt32("PuestoID");
                        DateTime fecha_inicioDb = reader.GetDateTime("Fecha_Inicio");
                        DateTime fecha_finDb = reader.GetDateTime("Fecha_Fin");
                        string estadoDb = reader["Estado"].ToString();
                        decimal costototalDb = reader.GetDecimal("CostoTotal");

                        return ( idDb, clienteidDb, puestoidDb, fecha_inicioDb, fecha_finDb, estadoDb, costototalDb);
                    }
                }
            }

            return null;
        }
        public bool AgregarReserva(int ClienteID, int PuestoID, DateTime Fecha_Inicio, DateTime Fecha_Fin, string Estado, decimal CostoTotal)
        {
            string query = "INSERT INTO reserva (ClienteID, PuestoID, Fecha_Inicio, Fecha_Fin, Estado, CostoTotal) VALUES (@ClienteID, @PuestoID, @Fecha_Inicio, @Fecha_Fin, @Estado, @CostoTotal)";

            using (MySqlConnection conexion = new MySqlConnection(_conexionString))
            {
                MySqlCommand comando = new MySqlCommand(query, conexion);

                comando.Parameters.AddWithValue("@ClienteID", ClienteID);
                comando.Parameters.AddWithValue("@PuestoID", PuestoID);
                comando.Parameters.AddWithValue("@Fecha_Inicio", Fecha_Inicio);
                comando.Parameters.AddWithValue("@Fecha_Fin", Fecha_Fin);
                comando.Parameters.AddWithValue("@Estado", Estado);
                comando.Parameters.AddWithValue("@CostoTotal", CostoTotal);

                conexion.Open();

                int FilasAfectadas = comando.ExecuteNonQuery();

                return FilasAfectadas > 0;
            }
        }
        public bool CancelarReserva(int ID)
        {
            string query = "UPDATE reserva SET Estado = 'Cancelada' WHERE ID = @ID";

            using (MySqlConnection conexion = new MySqlConnection(_conexionString))
            {
                MySqlCommand comando = new MySqlCommand(query, conexion);

                comando.Parameters.AddWithValue("@ID", ID);

                conexion.Open();

                int FilasAfectadas = comando.ExecuteNonQuery();

                return FilasAfectadas > 0;
            }
        }
        public List<(int ID, int ClienteID, int PuestoID, DateTime Fecha_Inicio, DateTime Fecha_Fin, string Estado, decimal CostoTotal)> BuscarReservasPorPuesto(int PuestoID)
        {
            string query = "SELECT ID, ClienteID, PuestoID, Fecha_Inicio, Fecha_Fin, Estado, CostoTotal FROM reserva WHERE PuestoID = @PuestoID AND Fecha_Inicio > NOW() AND Estado = 'Activa' ORDER BY Fecha_Inicio";

            List<(int ID, int ClienteID, int PuestoID, DateTime Fecha_Inicio, DateTime Fecha_Fin, string Estado, decimal CostoTotal)> reservas = new();

            using (MySqlConnection conexion = new MySqlConnection(_conexionString))
            {
                MySqlCommand comando = new MySqlCommand(query, conexion);

                comando.Parameters.AddWithValue("@PuestoID", PuestoID);

                conexion.Open();

                using (MySqlDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int idDb = reader.GetInt32("ID");
                        int clienteidDb = reader.GetInt32("ClienteID");
                        int puestoidDb = reader.GetInt32("PuestoID");
                        DateTime fecha_inicioDb = reader.GetDateTime("Fecha_Inicio");
                        DateTime fecha_finDb = reader.GetDateTime("Fecha_Fin");
                        string estadoDb = reader["Estado"].ToString();
                        decimal costototalDb = reader.GetDecimal("CostoTotal");

                        reservas.Add(( idDb, clienteidDb, puestoidDb, fecha_inicioDb, fecha_finDb, estadoDb, costototalDb));
                    }
                }
            }

            return reservas;
        }
    }
}