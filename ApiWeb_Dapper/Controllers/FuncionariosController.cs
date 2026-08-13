using ApiWeb_Dapper.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ApiWeb_Dapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionariosController : ControllerBase
    {
        private readonly IDbConnection _dbConnection;

        public FuncionariosController(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var funcionarios = _dbConnection.Query<Funcionario>(
                    @"SELECT 
                        id_funcionario, 
                        nm_funcionario, 
                        cargo_funcionario, 
                        cadastrado_em 
                      FROM Funcionario 
                      ORDER BY nm_funcionario");

                return Ok(funcionarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensagem = "Erro ao consultar os funcionários.",
                    erro = ex.Message
                });
            }
        }

        [HttpGet("byId/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var funcionario = _dbConnection
                    .QueryFirstOrDefault<Funcionario>(
                        @"SELECT 
                            id_funcionario, 
                            nm_funcionario, 
                            cargo_funcionario, 
                            cadastrado_em 
                          FROM Funcionario 
                          WHERE id_funcionario = @id",
                        new { id });

                if (funcionario == null)
                {
                    return NotFound(new
                    {
                        mensagem = "Funcionário não encontrado."
                    });
                }

                return Ok(funcionario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensagem = "Erro ao consultar o funcionário.",
                    erro = ex.Message
                });
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Funcionario funcionario)
        {
            try
            {
                if (funcionario == null)
                {
                    return BadRequest(new
                    {
                        mensagem = "Os dados do funcionário são obrigatórios."
                    });
                }

                if (string.IsNullOrWhiteSpace(funcionario.nm_funcionario))
                {
                    return BadRequest(new
                    {
                        mensagem = "O nome do funcionário é obrigatório."
                    });
                }

                if (string.IsNullOrWhiteSpace(funcionario.cargo_funcionario))
                {
                    return BadRequest(new
                    {
                        mensagem = "O cargo do funcionário é obrigatório."
                    });
                }

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

                var id = _dbConnection.ExecuteScalar<int>(
                    sql,
                    funcionario);

                funcionario.id_funcionario = id;

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = id },
                    funcionario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensagem = "Erro ao cadastrar o funcionário.",
                    erro = ex.Message
                });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Funcionario funcionario)
        {
            try
            {
                if (funcionario == null)
                {
                    return BadRequest(new
                    {
                        mensagem = "Os dados do funcionário são obrigatórios."
                    });
                }

                if (string.IsNullOrWhiteSpace(funcionario.nm_funcionario))
                {
                    return BadRequest(new
                    {
                        mensagem = "O nome do funcionário é obrigatório."
                    });
                }

                if (string.IsNullOrWhiteSpace(funcionario.cargo_funcionario))
                {
                    return BadRequest(new
                    {
                        mensagem = "O cargo do funcionário é obrigatório."
                    });
                }

                var funcionarioExistente = _dbConnection
                    .QueryFirstOrDefault<Funcionario>(
                        @"SELECT 
                            id_funcionario,
                            nm_funcionario,
                            cargo_funcionario,
                            cadastrado_em
                          FROM Funcionario
                          WHERE id_funcionario = @id",
                        new { id });

                if (funcionarioExistente == null)
                {
                    return NotFound(new
                    {
                        mensagem = "Funcionário não encontrado."
                    });
                }

                var sql = @"
                    UPDATE Funcionario
                    SET
                        nm_funcionario = @nm_funcionario,
                        cargo_funcionario = @cargo_funcionario
                    WHERE id_funcionario = @id";

                var linhasAfetadas = _dbConnection.Execute(
                    sql,
                    new
                    {
                        id,
                        funcionario.nm_funcionario,
                        funcionario.cargo_funcionario
                    });

                if (linhasAfetadas == 0)
                {
                    return BadRequest(new
                    {
                        mensagem = "Não foi possível alterar o funcionário."
                    });
                }

                funcionario.id_funcionario = id;
                funcionario.cadastrado_em = funcionarioExistente.cadastrado_em;

                return Ok(new
                {
                    mensagem = "Funcionário alterado com sucesso.",
                    funcionario
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensagem = "Erro ao alterar o funcionário.",
                    erro = ex.Message
                });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var funcionario = _dbConnection
                    .QueryFirstOrDefault<Funcionario>(
                        @"SELECT 
                            id_funcionario,
                            nm_funcionario,
                            cargo_funcionario,
                            cadastrado_em
                          FROM Funcionario
                          WHERE id_funcionario = @id",
                        new { id });

                if (funcionario == null)
                {
                    return NotFound(new
                    {
                        mensagem = "Funcionário não encontrado."
                    });
                }

                var sql = @"
                    DELETE FROM Funcionario
                    WHERE id_funcionario = @id";

                var linhasAfetadas = _dbConnection.Execute(
                    sql,
                    new { id });

                if (linhasAfetadas == 0)
                {
                    return BadRequest(new
                    {
                        mensagem = "Não foi possível excluir o funcionário."
                    });
                }

                return Ok(new
                {
                    mensagem = "Funcionário excluído com sucesso."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensagem = "Erro ao excluir o funcionário.",
                    erro = ex.Message
                });
            }
        }
    }
}