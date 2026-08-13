using ApiWeb_Dapper.Models;
using Dapper;
using System.Data;

namespace ApiWeb_Dapper.DAL
{
    public class FuncionarioDAL
    {
        private readonly IDbConnection _dbConnection;

        public FuncionarioDAL(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public IEnumerable<Funcionario> GetAll()
        {
            var sql = @"
                SELECT
                    id_funcionario,
                    nm_funcionario,
                    cargo_funcionario,
                    cadastrado_em
                FROM Funcionario
                ORDER BY nm_funcionario";

            return _dbConnection.Query<Funcionario>(sql);
        }

        public Funcionario? GetById(int id)
        {
            var sql = @"
                SELECT
                    id_funcionario,
                    nm_funcionario,
                    cargo_funcionario,
                    cadastrado_em
                FROM Funcionario
                WHERE id_funcionario = @id";

            return _dbConnection.QueryFirstOrDefault<Funcionario>(
                sql,
                new { id }
            );
        }

        public int Insert(Funcionario funcionario)
        {
            var sql = @"
                INSERT INTO Funcionario
                (
                    nm_funcionario,
                    cargo_funcionario,
                    cadastrado_em
                )
                VALUES
                (
                    @nm_funcionario,
                    @cargo_funcionario,
                    NOW()
                );

                SELECT LAST_INSERT_ID();";

            return _dbConnection.ExecuteScalar<int>(
                sql,
                funcionario
            );
        }

        public int Update(int id, Funcionario funcionario)
        {
            var sql = @"
                UPDATE Funcionario
                SET
                    nm_funcionario = @nm_funcionario,
                    cargo_funcionario = @cargo_funcionario
                WHERE id_funcionario = @id";

            return _dbConnection.Execute(
                sql,
                new
                {
                    id,
                    funcionario.nm_funcionario,
                    funcionario.cargo_funcionario
                }
            );
        }

        public int Delete(int id)
        {
            var sql = @"
                DELETE FROM Funcionario
                WHERE id_funcionario = @id";

            return _dbConnection.Execute(
                sql,
                new { id }
            );
        }
    }
}