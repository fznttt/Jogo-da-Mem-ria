using System;
using System.Windows.Forms;

public class Program
{
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        // Solicitar os nomes dos jogadores
        string jogador1 = Prompt("Nome do Jogador 1:");
        string jogador2 = Prompt("Nome do Jogador 2:");

        Application.Run(new FormJogoDaMemoria(jogador1, jogador2));
    }

    private static string Prompt(string texto)
    {
        Form prompt = new Form()
        {
            Width = 300,
            Height = 150,
            Text = texto
        };

        Label textLabel = new Label() { Left = 50, Top = 20, Text = texto };
        TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 200 };
        Button confirmation = new Button() { Text = "Ok", Left = 100, Width = 100, Top = 80 };
        confirmation.Click += (sender, e) => { prompt.Close(); };

        prompt.Controls.Add(textLabel);
        prompt.Controls.Add(textBox);
        prompt.Controls.Add(confirmation);
        prompt.ShowDialog();

        return textBox.Text;
    }
}
