using SimularPartida.Models;

class Program
{
    static void Main(string[] args)
    {
        // Configurar times
        Time timeCasa = new Time("Corinthians");
        Time timeVisitante = new Time("Racing-URU");

        // Adicionar jogadores aos times
        for (int i = 1; i <= 11; i++)
        {
            timeCasa.Jogadores.Add(new Jogador
            {
                Nome = "Jogador Casa " + i,
                Estamina = 100,
                HabilidadeAtaque = 70,
                HabilidadeDefesa = 60,
                HabilidadeMeioCampo = 65,
                Moral = 70,
                Lesionado = false
            });

            timeVisitante.Jogadores.Add(new Jogador
            {
                Nome = "Jogador Visitante " + i,
                Estamina = 100,
                HabilidadeAtaque = 65,
                HabilidadeDefesa = 70,
                HabilidadeMeioCampo = 60,
                Moral = 65,
                Lesionado = false
            });
        }

        // Criar partida
        Partida partida = new Partida(timeCasa, timeVisitante);

        // Simular 90 minutos
        for (int minuto = 1; minuto <= 90; minuto++)
        {
            partida.SimularMinuto();
            // Exibir minuto atual e eventos
            Console.Clear();
            Console.WriteLine($"Minuto: {partida.MinutoAtual}");
            Console.WriteLine($"Placar: {timeCasa.Nome} {partida.GolsCasa} x {partida.GolsVisitante} {timeVisitante.Nome}");
            Console.WriteLine("Eventos:");
            foreach (var evento in partida.Eventos)
            {
                Console.WriteLine(evento);
            }
            if (partida.MinutoAtual == 45 )
            {
                Console.WriteLine("Fim do Primeiro Tempo");
                System.Threading.Thread.Sleep(1000);
            }
            System.Threading.Thread.Sleep(500); // Pausa de meio segundo entre os minutos para simular o tempo passando
        }

        // Mostrar resultado final
        Console.WriteLine("\nResultado final:");
        Console.WriteLine($"{timeCasa.Nome} {partida.GolsCasa} x {partida.GolsVisitante} {timeVisitante.Nome}");
    }
}