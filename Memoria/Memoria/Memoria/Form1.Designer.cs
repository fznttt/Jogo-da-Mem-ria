using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;

public class FormJogoDaMemoria : Form
{
    private JogoDaMemoria jogo;
    private Button[] botoesCartas;
    private const int Tamanho = 4; // Tamanho do grid (4x4)
    private const int TotalCartas = Tamanho * Tamanho;
    private Label labelJogadorDaVez; // Label para mostrar quem é o jogador da vez
    private Label labelPontosJogador1; // Label para exibir os pontos do jogador 1
    private Label labelPontosJogador2; // Label para exibir os pontos do jogador 2

    public FormJogoDaMemoria(string jogador1, string jogador2)
    {
        InitializeComponent();
        jogo = new JogoDaMemoria(jogador1, jogador2);
        CriarBotoes();
        CriarLabelJogadorDaVez();
        CriarLabelsPontos(); // Chamada para criar labels de pontos
        AtualizarLabelJogadorDaVez(); // Atualiza o label inicialmente
        AtualizarLabelsPontos(); // Atualiza os labels de pontos inicialmente
    }

    private void CriarLabelJogadorDaVez()
    {
        labelJogadorDaVez = new Label
        {
            Text = $"É a vez de: {jogo.JogadorAtual.Nome}",
            Location = new Point(10, 10),
            AutoSize = true
        };
        Controls.Add(labelJogadorDaVez);
    }

    private void CriarLabelsPontos()
    {
        labelPontosJogador1 = new Label
        {
            Text = $"{jogo.Jogador1.Nome} Pontos: {jogo.Jogador1.Pontos}",
            Location = new Point(10, 30),
            AutoSize = true
        };

        labelPontosJogador2 = new Label
        {
            Text = $"{jogo.Jogador2.Nome} Pontos: {jogo.Jogador2.Pontos}",
            Location = new Point(10, 50),
            AutoSize = true
        };

        Controls.Add(labelPontosJogador1);
        Controls.Add(labelPontosJogador2);
    }

    private void CriarBotoes()
    {
        botoesCartas = new Button[TotalCartas];
        for (int i = 0; i < TotalCartas; i++)
        {
            botoesCartas[i] = new Button
            {
                Text = "X",
                Width = 80,
                Height = 80,
                Tag = i,
                Location = new Point(80 * (i % Tamanho), 80 * (i / Tamanho) + 80)
            };

            botoesCartas[i].Click += BotaoCarta_Click;
            Controls.Add(botoesCartas[i]);
        }
    }

    private async void BotaoCarta_Click(object sender, EventArgs e)
    {
        Button botao = (Button)sender;
        int indice = (int)botao.Tag;

        if (botao.Text == "X")
        {
            botao.Text = jogo.Cartas[indice].Valor; // Usar a propriedade Cartas
            botao.Enabled = false; // Desabilitar botão após escolha

            // Verificar se duas cartas foram selecionadas
            if (jogo.Jogar(indice))
            {
                // Se encontrou um par, manter as cartas visíveis
                AtualizarLabelsPontos(); // Atualiza a contagem de pontos após jogar
                if (jogo.VerificarVitoria())
                {
                    MessageBox.Show($"Parabéns {jogo.NomeVencedor()}, você venceu!");
                    ReiniciarJogo();
                }
            }
            else
            {
                // Esperar 1 segundo antes de desvirar as cartas erradas
                await Task.Delay(1000);
                DesvirarCartasErradas();
                AtualizarLabelJogadorDaVez(); // Atualiza a vez do jogador após desvirar
            }
        }
    }

    private void DesvirarCartasErradas()
    {
        foreach (var botao in botoesCartas)
        {
            int indice = (int)botao.Tag;
            if (botao.Text != "X" && !jogo.Cartas[indice].Revelada) // Se a carta não for "X" e não estiver revelada
            {
                botao.Text = "X"; // Desvirar a carta
                botao.Enabled = true; // Reabilitar botão
            }
        }
    }

    private void AtualizarLabelJogadorDaVez()
    {
        labelJogadorDaVez.Text = $"É a vez de: {jogo.JogadorAtual.Nome}";
    }

    private void AtualizarLabelsPontos()
    {
        labelPontosJogador1.Text = $"{jogo.Jogador1.Nome} Pontos: {jogo.Jogador1.Pontos}";
        labelPontosJogador2.Text = $"{jogo.Jogador2.Nome} Pontos: {jogo.Jogador2.Pontos}";
    }

    private void ReiniciarJogo()
    {
        // Resetar o jogo
        jogo = new JogoDaMemoria(jogo.Jogador1.Nome, jogo.Jogador2.Nome);
        ReiniciarBotoes();
        AtualizarLabelJogadorDaVez(); // Atualiza a vez do jogador
        AtualizarLabelsPontos(); // Atualiza os labels de pontos
    }

    private void ReiniciarBotoes()
    {
        foreach (var botao in botoesCartas)
        {
            botao.Text = "X"; // Resetar texto das cartas
            botao.Enabled = true; // Reabilitar botão
        }
    }

    private void InitializeComponent()
    {
        this.SuspendLayout();
        this.ClientSize = new Size(400, 400);
        this.Name = "FormJogoDaMemoria";
        this.Text = "Jogo da Memória";
        this.ResumeLayout(false);
    }
}
