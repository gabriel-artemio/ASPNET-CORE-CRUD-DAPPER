using SimularPartida.Models;

public class Partida
{
    public Time TimeCasa { get; set; }
    public Time TimeVisitante { get; set; }
    public string? Clima { get; set; }
    public string? EstadoCampo { get; set; }
    public int MinutoAtual { get; set; }
    public int GolsCasa { get; set; }
    public int GolsVisitante { get; set; }
    public List<string> Eventos { get; set; }

    private Random rand = new Random();

    public Partida(Time timeCasa, Time timeVisitante)
    {
        TimeCasa = timeCasa;
        TimeVisitante = timeVisitante;
        MinutoAtual = 0;
        GolsCasa = 0;
        GolsVisitante = 0;
        Eventos = new List<string>();
    }

    public void SimularMinuto()
    {
        // Atualiza o minuto da partida
        MinutoAtual++;

        // Checar o estado físico e moral dos jogadores
        foreach (var jogador in TimeCasa.Jogadores)
        {
            if (jogador.Lesionado)
            {
                continue; // Ignora jogadores lesionados
            }

            // Reduz a estamina dos jogadores
            jogador.Estamina -= rand.Next(1, 3);
            if (jogador.Estamina < 0) jogador.Estamina = 0;
        }

        foreach (var jogador in TimeVisitante.Jogadores)
        {
            if (jogador.Lesionado)
            {
                continue; // Ignora jogadores lesionados
            }

            // Reduz a estamina dos jogadores
            jogador.Estamina -= rand.Next(1, 3);
            if (jogador.Estamina < 0) jogador.Estamina = 0;
        }

        // Simulação de eventos durante o minuto
        int evento = rand.Next(1, 101); // Gera um número aleatório entre 1 e 100

        if (evento <= 10) // 10% de chance de ocorrer um evento significativo (gol, falta, etc.)
        {
            int tipoEvento = rand.Next(1, 101);

            if (tipoEvento <= 40) // 40% de chance de ser um ataque perigoso
            {
                SimularAtaque();
            }
            else if (tipoEvento <= 70) // 30% de chance de ser uma falta
            {
                SimularFalta();
            }
            else // 30% de chance de outro evento (cartão, substituição, etc.)
            {
                SimularOutrosEventos();
            }
        }
    }

    private void SimularAtaque()
    {
        // Determine qual time está atacando
        bool ataqueCasa = rand.Next(0, 2) == 0;

        Time timeAtacante = ataqueCasa ? TimeCasa : TimeVisitante;
        Time timeDefensor = ataqueCasa ? TimeVisitante : TimeCasa;

        // Calcular a força do ataque e da defesa
        int forcaAtaque = timeAtacante.Jogadores.Sum(j => j.HabilidadeAtaque + j.Moral + (j.Estamina / 10));
        int forcaDefesa = timeDefensor.Jogadores.Sum(j => j.HabilidadeDefesa + j.Moral + (j.Estamina / 10));

        // Verificar se o ataque resulta em gol
        if (forcaAtaque > forcaDefesa + rand.Next(0, 101))
        {
            if (ataqueCasa)
            {
                GolsCasa++;
                RegistrarEvento($"{MinutoAtual}': Gol do {TimeCasa.Nome}!");
            }
            else
            {
                GolsVisitante++;
                RegistrarEvento($"{MinutoAtual}': Gol do {TimeVisitante.Nome}!");
            }
        }
    }

    private void SimularFalta()
    {
        // Determine qual time cometeu a falta
        bool faltaCasa = rand.Next(0, 2) == 0;

        Time timeFaltoso = faltaCasa ? TimeCasa : TimeVisitante;
        Time timeBeneficiado = faltaCasa ? TimeVisitante : TimeCasa;

        // Calcular a chance de cartão
        int chanceCartao = rand.Next(1, 101);

        if (chanceCartao <= 30) // 30% de chance de cartão amarelo
        {
            // Selecionar jogador faltoso e aplicar cartão
            Jogador jogadorFaltoso = timeFaltoso.Jogadores[rand.Next(timeFaltoso.Jogadores.Count)];
            jogadorFaltoso.Moral -= 5; // Reduz a moral do jogador
            RegistrarEvento($"{MinutoAtual}': Cartão amarelo para {jogadorFaltoso.Nome} ({timeFaltoso.Nome})");
        }
        else if (chanceCartao <= 10) // 10% de chance de cartão vermelho
        {
            // Selecionar jogador faltoso e aplicar cartão
            Jogador jogadorFaltoso = timeFaltoso.Jogadores[rand.Next(timeFaltoso.Jogadores.Count)];
            jogadorFaltoso.Lesionado = true; // Expulsar jogador do jogo
            RegistrarEvento($"{MinutoAtual}': Cartão vermelho para {jogadorFaltoso.Nome} ({timeFaltoso.Nome})");
        }

        // Simular cobrança de falta
        int resultadoFalta = rand.Next(1, 101);

        if (resultadoFalta <= 15) // 15% de chance de gol na cobrança de falta
        {
            if (faltaCasa)
            {
                GolsVisitante++;
                RegistrarEvento($"{MinutoAtual}': Gol de falta do {TimeVisitante.Nome}!");
            }
            else
            {
                GolsCasa++;
                RegistrarEvento($"{MinutoAtual}': Gol de falta do {TimeCasa.Nome}!");
            }
        }
    }

    private void SimularOutrosEventos()
    {
        // Exemplos de outros eventos:
        // - Substituição de jogadores
        // - Contusão de jogadores
        // - Mudança tática

        int tipoOutroEvento = rand.Next(1, 101);

        if (tipoOutroEvento <= 50) // 50% de chance de substituição
        {
            // Realizar substituição
            // Implementar lógica de substituição de jogadores
            RegistrarEvento($"{MinutoAtual}': Substituição realizada.");
        }
        else if (tipoOutroEvento <= 80) // 30% de chance de contusão
        {
            // Selecionar jogador aleatório e aplicar contusão
            bool lesaoCasa = rand.Next(0, 2) == 0;
            Time timeLesao = lesaoCasa ? TimeCasa : TimeVisitante;
            Jogador jogadorLesionado = timeLesao.Jogadores[rand.Next(timeLesao.Jogadores.Count)];
            jogadorLesionado.Lesionado = true;
            RegistrarEvento($"{MinutoAtual}': Contusão de {jogadorLesionado.Nome} ({timeLesao.Nome})");
        }
        else // 20% de chance de mudança tática
        {
            // Implementar lógica de mudança tática
            RegistrarEvento($"{MinutoAtual}': Mudança tática realizada.");
        }
    }

    private void RegistrarEvento(string evento)
    {
        Eventos.Add(evento);
    }
}
