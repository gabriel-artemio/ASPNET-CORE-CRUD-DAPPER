using System;
using System.Collections.Generic;

namespace futebol.Models
{
    class Program
    {
        static List<Clubes> listaClubes = new List<Clubes>();
        static string[,] calendarioJogos; // Declaração do calendário de jogos

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Selecione uma opção:");
                Console.WriteLine("1. Participantes do Campeonato Brasileiro");
                Console.WriteLine("2. Gerar Calendário de Jogos");
                Console.WriteLine("3. Ver Calendário");
                Console.WriteLine("4. Sair");

                // Lê a entrada do usuário
                string input = Console.ReadLine();

                // Verifica a opção escolhida
                switch (input)
                {
                    case "1":
                        Opcao1();
                        break;
                    case "2":
                        Opcao2();
                        break;
                    case "3":
                        Opcao3();
                        break;
                    case "4":
                        Console.WriteLine("Saindo...");
                        return;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }

                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey(true);
                Console.Clear();
            }
        }

        static void Opcao1()
        {
            Console.WriteLine("Campeonato Brasileiro 2024");
            // Adicionando clubes à lista
            AdicionarClubes();

            // Embaralhando a ordem dos clubes
            EmbaralharClubes();

            // Exibindo os clubes embaralhados
            foreach (var clube in listaClubes)
            {
                Console.WriteLine(clube.nm_clube + " - " + clube.nm_estadio);
            }
        }

        static void Opcao2()
        {
            Console.WriteLine("Gerar Calendário de Jogos");
            if (listaClubes == null)
            {
                Console.WriteLine("Os times ainda não foram inseridos. Por favor, gere-os primeiro.");
                return;
            }
            else
            {
                // Definindo o número de competidores
                int numeroCompetidores = listaClubes.Count;

                // Criando um array de competidores
                string[] competidores = new string[numeroCompetidores];

                // Preenchendo o array de competidores com os nomes dos clubes
                for (int i = 0; i < numeroCompetidores; i++)
                {
                    competidores[i] = listaClubes[i].nm_clube;
                }

                // Inicializando o calendário de jogos
                calendarioJogos = new string[numeroCompetidores, numeroCompetidores - 1];

                // Preenchendo o calendário de jogos com os emparelhamentos
                for (int rodada = 0; rodada < numeroCompetidores - 1; rodada++)
                {
                    for (int i = 0; i < numeroCompetidores / 2; i++)
                    {
                        calendarioJogos[i, rodada] = competidores[i];
                        calendarioJogos[numeroCompetidores - 1 - i, rodada] = competidores[numeroCompetidores - 1 - i];
                    }
                    RotateCompetidores(competidores);
                }

                Console.WriteLine("Calendário de jogos gerado com sucesso!");
            }
        }

        static void Opcao3()
        {
            Console.WriteLine("Calendário de Jogos");

            // Verifica se o calendário de jogos foi gerado
            if (calendarioJogos == null)
            {
                Console.WriteLine("O calendário de jogos ainda não foi gerado. Por favor, gere-o primeiro.");
                return;
            }

            // Imprimindo o calendário de jogos
            for (int rodada = 0; rodada < listaClubes.Count - 1; rodada++)
            {
                Console.WriteLine($"Rodada {rodada + 1}:");
                for (int i = 0; i < listaClubes.Count / 2; i++)
                {
                    Console.WriteLine($"{calendarioJogos[i, rodada]} x {calendarioJogos[listaClubes.Count - 1 - i, rodada]}");
                }
                Console.WriteLine();
            }
        }

        // Método para girar os competidores para a próxima rodada
        static void RotateCompetidores(string[] competidores)
        {
            string ultimoCompetidor = competidores[competidores.Length - 1];
            for (int i = competidores.Length - 1; i > 1; i--)
            {
                competidores[i] = competidores[i - 1];
            }
            competidores[1] = ultimoCompetidor;
        }

        static void AdicionarClubes()
        {
            listaClubes = new List<Clubes>
        {
            new Clubes() { nm_clube = "Corinthians", nm_estadio = "Neo Química Arena" },
            new Clubes() { nm_clube = "Athlético-PR", nm_estadio = "Arena da Baixada" },
            new Clubes() { nm_clube = "Atlético-MG", nm_estadio = "Arena MRV" },
            new Clubes() { nm_clube = "Bahia", nm_estadio = "Arena Fonte Nova" },
            new Clubes() { nm_clube = "Cruzeiro", nm_estadio = "Mineirão" },
            new Clubes() { nm_clube = "Grêmio", nm_estadio = "Arena do Grêmio" },
            new Clubes() { nm_clube = "Internacional", nm_estadio = "Beira Rio" },
            new Clubes() { nm_clube = "Juventude", nm_estadio = "Alfredo Jaconi" },
            new Clubes() { nm_clube = "Palmeiras", nm_estadio = "Allianz Parque" },
            new Clubes() { nm_clube = "São Paulo", nm_estadio = "MorumBIS" },
            new Clubes() { nm_clube = "Criciúma", nm_estadio = "Heriberto Hulse" },
            new Clubes() { nm_clube = "Vitória", nm_estadio = "Barradão" },
            new Clubes() { nm_clube = "Flamengo", nm_estadio = "Maracanã" },
            new Clubes() { nm_clube = "Fluminense", nm_estadio = "Maracanã" },
            new Clubes() { nm_clube = "Vasco", nm_estadio = "São Januário" },
            new Clubes() { nm_clube = "Botafogo", nm_estadio = "Nilton Santos" },
            new Clubes() { nm_clube = "Fortaleza", nm_estadio = "Castelão" },
            new Clubes() { nm_clube = "Cuiabá", nm_estadio = "Arena Pantanal" },
            new Clubes() { nm_clube = "RB Bragantino", nm_estadio = "Nabi Abi Chedid" },
            new Clubes() { nm_clube = "Atlético-GO", nm_estadio = "Antonio Accioly" },
        };
        }

        static void EmbaralharClubes()
        {
            Random rng = new Random();
            int n = listaClubes.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Clubes value = listaClubes[k];
                listaClubes[k] = listaClubes[n];
                listaClubes[n] = value;
            }
        }
    }
}