using ApiWeb_Dapper.BLL;
using ApiWeb_Dapper.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiWeb_Dapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionariosController : ControllerBase
    {
        private readonly FuncionarioBLL _funcionarioBLL;

        public FuncionariosController(FuncionarioBLL funcionarioBLL)
        {
            _funcionarioBLL = funcionarioBLL;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var funcionarios =
                    _funcionarioBLL.GetAll();

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
                var funcionario =
                    _funcionarioBLL.GetById(id);

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
        public IActionResult Post(
            [FromBody] Funcionario funcionario)
        {
            try
            {
                var id =
                    _funcionarioBLL.Insert(funcionario);

                funcionario.id_funcionario = id;

                return CreatedAtAction(
                    nameof(GetById),
                    new { id },
                    funcionario
                );
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    mensagem = ex.Message
                });
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
        public IActionResult Put(
            int id,
            [FromBody] Funcionario funcionario)
        {
            try
            {
                var atualizado =
                    _funcionarioBLL.Update(
                        id,
                        funcionario
                    );

                if (!atualizado)
                {
                    return NotFound(new
                    {
                        mensagem = "Funcionário não encontrado."
                    });
                }

                var funcionarioAtualizado =
                    _funcionarioBLL.GetById(id);

                return Ok(new
                {
                    mensagem =
                        "Funcionário alterado com sucesso.",
                    funcionario = funcionarioAtualizado
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    mensagem = ex.Message
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
                var excluido =
                    _funcionarioBLL.Delete(id);

                if (!excluido)
                {
                    return NotFound(new
                    {
                        mensagem = "Funcionário não encontrado."
                    });
                }

                return Ok(new
                {
                    mensagem =
                        "Funcionário excluído com sucesso."
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