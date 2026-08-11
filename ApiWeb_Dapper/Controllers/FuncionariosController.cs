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
                var funcionarios = _dbConnection.Query<Funcionario>(@"SELECT id_funcionario, nm_funcionario, cargo_funcionario, cadastrado_em FROM Funcionario ORDER BY nm_funcionario");
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
                    .QueryFirstOrDefault<Funcionario>(@"SELECT id_funcionario, nm_funcionario, cargo_funcionario, 
                        cadastrado_em FROM Funcionario WHERE id_funcionario = @id", new{id});

                if (funcionario == null)
                {
                    return NotFound(new { mensagem = "Funcionário não encontrado." });
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
    }
}