using ApiWeb_Dapper.DAL;
using ApiWeb_Dapper.Models;

namespace ApiWeb_Dapper.BLL
{
    public class FuncionarioBLL
    {
        private readonly FuncionarioDAL _funcionarioDAL;

        public FuncionarioBLL(FuncionarioDAL funcionarioDAL)
        {
            _funcionarioDAL = funcionarioDAL;
        }

        public IEnumerable<Funcionario> GetAll()
        {
            return _funcionarioDAL.GetAll();
        }

        public Funcionario? GetById(int id)
        {
            if (id <= 0)
                return null;

            return _funcionarioDAL.GetById(id);
        }

        public int Insert(Funcionario funcionario)
        {
            if (funcionario == null)
                throw new ArgumentException(
                    "Os dados do funcionário são obrigatórios."
                );

            if (string.IsNullOrWhiteSpace(funcionario.nm_funcionario))
                throw new ArgumentException(
                    "O nome do funcionário é obrigatório."
                );

            if (string.IsNullOrWhiteSpace(funcionario.cargo_funcionario))
                throw new ArgumentException(
                    "O cargo do funcionário é obrigatório."
                );

            funcionario.nm_funcionario = funcionario.nm_funcionario.Trim();

            funcionario.cargo_funcionario = funcionario.cargo_funcionario.Trim();

            return _funcionarioDAL.Insert(funcionario);
        }

        public bool Update(int id, Funcionario funcionario)
        {
            if (id <= 0)
                return false;

            if (funcionario == null)
                throw new ArgumentException(
                    "Os dados do funcionário são obrigatórios."
                );

            if (string.IsNullOrWhiteSpace(funcionario.nm_funcionario))
                throw new ArgumentException(
                    "O nome do funcionário é obrigatório."
                );

            if (string.IsNullOrWhiteSpace(funcionario.cargo_funcionario))
                throw new ArgumentException(
                    "O cargo do funcionário é obrigatório."
                );

            var funcionarioExistente = _funcionarioDAL.GetById(id);

            if (funcionarioExistente == null)
                return false;

            funcionario.nm_funcionario = funcionario.nm_funcionario.Trim();

            funcionario.cargo_funcionario = funcionario.cargo_funcionario.Trim();

            var linhasAfetadas =  _funcionarioDAL.Update(id, funcionario);

            return linhasAfetadas > 0;
        }

        public bool Delete(int id)
        {
            if (id <= 0)
                return false;

            var funcionario = _funcionarioDAL.GetById(id);

            if (funcionario == null)
                return false;

            var linhasAfetadas = _funcionarioDAL.Delete(id);

            return linhasAfetadas > 0;
        }
    }
}