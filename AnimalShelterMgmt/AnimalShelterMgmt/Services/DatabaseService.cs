using MySql.Data.MySqlClient;

namespace AnimalShelterMgmt.Services
{
    public class DatabaseService
    {
        private readonly string connectionString = "Server=localhost;Database=animalsheltermgmt;Uid=root;Pwd=;SslMode=none;";

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}