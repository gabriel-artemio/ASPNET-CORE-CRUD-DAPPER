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
        char[] competidores = new char[numeroCompetidores];

        // Preenchendo o array de competidores com letras de A a partir do número de competidores
        for (int i = 0; i < numeroCompetidores; i++)
        {
            competidores[i] = (char)('A' + i);
        }

        // Criando o calendário de jogos
        char[,] calendarioJogos = new char[numeroCompetidores, numeroCompetidores - 1];

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
    static void RotateCompetidores(char[] competidores)
    {
        char temp = competidores[competidores.Length - 1];
        for (int i = competidores.Length - 1; i > 1; i--)
        {
            competidores[i] = competidores[i - 1];
        }
        competidores[1] = temp;
    }
}