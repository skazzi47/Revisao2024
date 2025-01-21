using System;
using System.IO;
using System.Text;

public class GeradorDeSenhasSeguras
{
    // Função para gerar uma senha aleatória
    public static string GerarSenha(int tamanho, bool apenasNumeros, bool incluirLetras, bool incluirSimbolos)
    {
        var caracteresPermitidos = new StringBuilder();
        
        // Adicionando números, letras e símbolos ao conjunto permitido
        if (apenasNumeros)
        {
            caracteresPermitidos.Append("0123456789");
        }
        if (incluirLetras)
        {
            caracteresPermitidos.Append("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ");
        }
        if (incluirSimbolos)
        {
            caracteresPermitidos.Append("@!#$%-&*()");
        }

        var senha = new char[tamanho];
        var random = new Random();
        
        for (int i = 0; i < tamanho; i++)
        {
            int indice = random.Next(caracteresPermitidos.Length);
            senha[i] = caracteresPermitidos[indice];
        }

        return new string(senha);
    }

    // Função para salvar a senha em um arquivo
    public static void SalvarSenha(string senha)
    {
        string caminhoArquivo = "bkp.txt";
        
        // Adiciona a senha no arquivo bkp.txt
        using (StreamWriter sw = new StreamWriter(caminhoArquivo, true))
        {
            sw.WriteLine(senha);
        }
    }

    // Função para recuperar senhas do arquivo
    public static void RecuperarSenhas()
    {
        string caminhoArquivo = "bkp.txt";
        
        if (File.Exists(caminhoArquivo))
        {
            using (StreamReader sr = new StreamReader(caminhoArquivo))
            {
                string linha;
                Console.WriteLine("Senhas salvas: ");
                while ((linha = sr.ReadLine()) != null)
                {
                    Console.WriteLine(linha);
                }
            }
        }
        else
        {
            Console.WriteLine("Nenhuma senha salva foi encontrada.");
        }
    }

    public static void Main()
    {
        Console.WriteLine("Gerador de Senhas Seguras");

        // Entradas do usuário
        Console.Write("Digite o tamanho da senha: ");
        int tamanho = int.Parse(Console.ReadLine());

        Console.Write("Deseja incluir apenas números? (S/N): ");
        bool apenasNumeros = Console.ReadLine().ToLower() == "s";

        Console.Write("Deseja incluir letras (maiúsculas e minúsculas)? (S/N): ");
        bool incluirLetras = Console.ReadLine().ToLower() == "s";

        Console.Write("Deseja incluir caracteres especiais (@, !, #, -)? (S/N): ");
        bool incluirSimbolos = Console.ReadLine().ToLower() == "s";

        // Gerando a senha
        string senhaGerada = GerarSenha(tamanho, apenasNumeros, incluirLetras, incluirSimbolos);

        // Exibindo a senha gerada
        Console.WriteLine($"\nSua senha foi gerada: {senhaGerada}");

        // Salvando a senha no arquivo de backup
        SalvarSenha(senhaGerada);
        Console.WriteLine("Senha salva em bkp.txt.");

        // Pergunta para recuperar senhas
        Console.WriteLine("\nDeseja ver as senhas salvas? (S/N): ");
        if (Console.ReadLine().ToLower() == "s")
        {
            RecuperarSenhas();
        }
    }
}
