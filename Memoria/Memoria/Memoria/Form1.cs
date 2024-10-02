using System;
using System.Collections.Generic;

public class JogoDaMemoria
{
    public List<Carta> Cartas { get; private set; }
    public Jogador Jogador1 { get; private set; }
    public Jogador Jogador2 { get; private set; }
    public Jogador JogadorAtual { get; private set; }
    private int primeiraEscolha = -1;
    private int segundaEscolha = -1;

    public JogoDaMemoria(string nomeJogador1, string nomeJogador2)
    {
        Jogador1 = new Jogador(nomeJogador1);
        Jogador2 = new Jogador(nomeJogador2);
        JogadorAtual = Jogador1; // Começa com o jogador 1

        Cartas = CriarCartas();
    }

    private List<Carta> CriarCartas()
    {
        List<Carta> cartas = new List<Carta>();
        string[] valores = { "A", "B", "C", "D", "E", "F", "G", "H" }; // Valores das cartas

        // Adiciona cada valor duas vezes (par)
        foreach (var valor in valores)
        {
            cartas.Add(new Carta(valor));
            cartas.Add(new Carta(valor));
        }

        // Embaralhar as cartas
        Random random = new Random();
        for (int i = 0; i < cartas.Count; i++)
        {
            int j = random.Next(i, cartas.Count);
            var temp = cartas[i];
            cartas[i] = cartas[j];
            cartas[j] = temp;
        }

        return cartas;
    }

    public bool Jogar(int indice)
    {
        if (primeiraEscolha == -1)
        {
            primeiraEscolha = indice;
            Cartas[indice].Revelada = true;
            return false;
        }
        else
        {
            segundaEscolha = indice;
            Cartas[indice].Revelada = true;

            if (Cartas[primeiraEscolha].Valor == Cartas[segundaEscolha].Valor)
            {
                // Par encontrado
                AdicionarPonto();
                primeiraEscolha = -1; // Resetar
                segundaEscolha = -1;
                return true;
            }
            else
            {
                // Não encontrou par
                Cartas[primeiraEscolha].Revelada = false; // Desvira
                Cartas[segundaEscolha].Revelada = false; // Desvira
                primeiraEscolha = -1; // Resetar
                segundaEscolha = -1;
                TrocarJogador();
                return false;
            }
        }
    }

    private void AdicionarPonto()
    {
        JogadorAtual.AdicionarPonto();
    }

    private void TrocarJogador()
    {
        JogadorAtual = JogadorAtual == Jogador1 ? Jogador2 : Jogador1;
    }

    public bool VerificarVitoria()
    {
        return Cartas.TrueForAll(carta => carta.Revelada);
    }

    public string NomeVencedor()
    {
        return Jogador1.Pontos > Jogador2.Pontos ? Jogador1.Nome : Jogador2.Nome;
    }
}
