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
                temp1 REAL,
                temp2 REAL,
                tempExterna REAL,
                porta INTEGER,
                processado INTEGER DEFAULT 0,
                timestamp DATETIME DEFAULT CURRENT_TIMESTAMP,
                hora INTEGER
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
            INSERT INTO dados_geladeira (temp1, temp2, tempExterna, porta, hora)
            VALUES (@temp1, @temp2, @tempExterna, @porta, @hora);
            ";

            command.Parameters.AddWithValue("@temp1", data.temp1);
            command.Parameters.AddWithValue("@temp2", data.temp2);
            command.Parameters.AddWithValue("@tempExterna", data.tempExterna);
            command.Parameters.AddWithValue("@porta", data.porta ? 1 : 0);
            command.Parameters.AddWithValue("@hora", data.hora);

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
            SELECT id, temp1, temp2, tempExterna, porta, processado, timestamp, hora
            FROM dados_geladeira
            ORDER BY timestamp DESC
            LIMIT 50;
            ";

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new DadosGeladeira
                {
                    id = reader.GetInt32(0),
                    temp1 = reader.GetDouble(1),
                    temp2 = reader.GetDouble(2),
                    tempExterna = reader.GetDouble(3),
                    porta = reader.GetBoolean(4),
                    processado = reader.GetInt32(5),
                    timestamp = reader.GetDateTime(6),
                    hora = reader.GetInt32(7)
                });
            }

            return list;
        }
    }
}
