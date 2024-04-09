using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Número de competidores:");
        int numeroCompetidores = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Deseja gerar turno e returno?");
        Console.WriteLine("1 - SIM | 2 - NÃO");
        int turnoCampeonato = Convert.ToInt32(Console.ReadLine());

        // Calculando o número total de jogos
        int numeroJogos = (numeroCompetidores * (numeroCompetidores - 1)) / 2;

        // Criando um array de competidores
        string[] competidores = new string[numeroCompetidores];

        // Recebendo os nomes dos competidores
        for (int i = 0; i < numeroCompetidores; i++)
        {
            Console.WriteLine($"Digite o nome do competidor {i + 1}:");
            competidores[i] = Console.ReadLine();
        }

        // Criando o calendário de jogos
        string[,] calendarioJogos = new string[numeroCompetidores, numeroCompetidores - 1];

        // Preenchendo o calendário de jogos com os emparelhamentos
        for (int rodada = 0; rodada < numeroCompetidores - 1; rodada++)
        {
            // Definindo os emparelhamentos para esta rodada
            for (int i = 0; i < numeroCompetidores / 2; i++)
            {
                calendarioJogos[i, rodada] = competidores[i];
                calendarioJogos[numeroCompetidores - 1 - i, rodada] = competidores[numeroCompetidores - 1 - i];
            }

            // Girando os competidores para a próxima rodada
            RotateCompetidores(competidores);
        }

        // Imprimindo o calendário de jogos
        for (int rodada = 0; rodada < numeroCompetidores - 1; rodada++)
        {
            Console.WriteLine($"Rodada {rodada + 1}:");
            for (int i = 0; i < numeroCompetidores / 2; i++)
            {
                Console.WriteLine($"{calendarioJogos[i, rodada]} vs {calendarioJogos[numeroCompetidores - 1 - i, rodada]}");
            }
            Console.WriteLine();
        }
    }

    // Método para girar os competidores
    static void RotateCompetidores(string[] competidores)
    {
        string temp = competidores[competidores.Length - 1];
        for (int i = competidores.Length - 1; i > 1; i--)
        {
            competidores[i] = competidores[i - 1];
        }
        competidores[1] = temp;
    }
}