using System;

public class ValidacaoProdutos
{
    public static bool ValidarProduto(string nomeProduto, decimal precoProduto, int quantidadeEstoque)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(nomeProduto))
            {
                throw new ArgumentException("Erro: O nome do produto não pode ser vazio.");
            }

            if (precoProduto <= 0)
            {
                throw new ArgumentException("Erro: O preço do produto tem que ser maior que zero.");
            }

            if (quantidadeEstoque < 0)
            {
                throw new ArgumentException("Erro: A quantidade em estoque não pode ser negativa.");
            }

            return true;
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro inesperado: {ex.Message}");
            return false;
        }
    }

    internal class ValidadorProduto
    {
    }
}
