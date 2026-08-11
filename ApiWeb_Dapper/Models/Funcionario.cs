namespace ApiWeb_Dapper.Models
{
    public class Funcionario
    {
        public int id_funcionario { get; set; }
        public string? nm_funcionario { get; set; }
        public string? cargo_funcionario { get; set; }
        public DateTime cadastrado_em { get; set; }
    }
}