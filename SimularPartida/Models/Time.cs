namespace SimularPartida.Models
{
    public class Time
    {
        public string? Nome { get; set; }
        public List<Jogador> Jogadores { get; set; }
        public int FormaRecente { get; set; }
        public int Motivacao { get; set; }
        public string? Formacao { get; set; }

        public Time(string nome)
        {
            Nome = nome;
            Jogadores = new List<Jogador>();
        }
    }
}