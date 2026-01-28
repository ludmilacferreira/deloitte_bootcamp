public class ValidacaoProdutos
{
    public static bool ValidarProduto(string nomeProduto, decimal precoProduto, int quantidadeEstoque)
    {
        if (string.IsNullOrWhiteSpace(nomeProduto))
        {
            Console.WriteLine("Erro: O nome do produto não pode ser vazio.");
            return false;
        }

        if (precoProduto <= 0)
        {
            Console.WriteLine("Erro: O preço do produto tem que ser maior que zero.");
            return false;
        }

        if (quantidadeEstoque < 0)
        {
            Console.WriteLine("A quantidade em estoque não pode ser negativa.");
            return false;
        }

            return true;
    }

    internal class ValidadorProduto
    {
    }
}
