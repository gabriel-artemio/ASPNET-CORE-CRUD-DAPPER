namespace SimularPartida.Models
{
    public class Jogador
    {
        public string? Nome { get; set; }
        public int Estamina { get; set; }
        public int HabilidadeAtaque { get; set; }
        public int HabilidadeDefesa { get; set; }
        public int HabilidadeMeioCampo { get; set; }
        public int Moral { get; set; }
        public bool Lesionado { get; set; }
    }
}