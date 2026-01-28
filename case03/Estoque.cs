public class Estoque
{
    public string NomeProduto { get; private set; }
    public decimal PrecoProduto { get; private set; }
    public int QuantidadeEstoque { get; private set; }

    public Estoque(string nomeProduto, decimal precoProduto, int quantidadeEstoque)
    {
        NomeProduto = nomeProduto;
        PrecoProduto = precoProduto;
        QuantidadeEstoque = quantidadeEstoque;
    }
}