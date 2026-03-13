using ApiDotnet_TCC.Models;
using Microsoft.Data.Sqlite;

namespace ApiDotnet_TCC.Data
{
    public class Db
    {
        private readonly string _connectionString = "Data Source=geladeira.db";

        public void Initialize()
        {
            using var connection = new SqliteConnection(_connectionString);

            connection.Open();

            var command = connection.CreateCommand();

            command.CommandText =
            @"
            CREATE TABLE IF NOT EXISTS dados_geladeira (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                temp_sensor_1 REAL,
                temp_sensor_2 REAL,
                temp_sensor_externo REAL,
                porta_aberta INTEGER,
                timestamp DATETIME DEFAULT CURRENT_TIMESTAMP
            );
            ";

            command.ExecuteNonQuery();
        }

        public void Insert(DadosGeladeira data)
        {
            using var connection = new SqliteConnection(_connectionString);

            connection.Open();

            var command = connection.CreateCommand();

            command.CommandText =
            @"
            INSERT INTO dados_geladeira (temp_sensor_1, temp_sensor_2, temp_sensor_externo, porta_aberta)
            VALUES ($temp_sensor_1, $temp_sensor_2, $temp_sensor_externo, $porta_aberta);
            ";

            command.Parameters.AddWithValue("temp_sensor_1", data.temp_sensor_1);
            command.Parameters.AddWithValue("temp_sensor_2", data.temp_sensor_2);
            command.Parameters.AddWithValue("temp_sensor_externo", data.temp_sensor_externo);
            command.Parameters.AddWithValue("porta_aberta", data.porta_aberta ? 1 : 0);

            command.ExecuteNonQuery();
        }

        public List<DadosGeladeira> GetLast()
        {
            var list = new List<DadosGeladeira>();

            using var connection = new SqliteConnection(_connectionString);

            connection.Open();

            var command = connection.CreateCommand();

            command.CommandText =
            @"
            SELECT temp_sensor_1, temp_sensor_2, temp_sensor_externo, porta_aberta, timestamp
            FROM dados_geladeira
            ORDER BY timestamp DESC
            LIMIT 50;
            ";

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new DadosGeladeira
                {
                    temp_sensor_1 = reader.GetDouble(0),
                    temp_sensor_2 = reader.GetDouble(1),
                    temp_sensor_externo = reader.GetDouble(2),
                    porta_aberta = reader.GetBoolean(3),
                    timestamp = reader.GetDateTime(4)
                });
            }

            return list;
        }
    }
}
