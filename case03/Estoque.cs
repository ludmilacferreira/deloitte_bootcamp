public class Estoque
{
    public string NomeProduto { get; set; }
    public decimal PrecoProduto { get; set; }
    public int QuantidadeEstoque { get; set; }

    public Estoque(string nomeProduto, decimal precoProduto, int quantidadeEstoque)
    {
        NomeProduto = nomeProduto;
        PrecoProduto = precoProduto;
        QuantidadeEstoque = quantidadeEstoque;
    }
}