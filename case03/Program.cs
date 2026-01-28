using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        List<Estoque> produtos = new List<Estoque>();

        Console.WriteLine("Digite o nome do produto:");
        string nomeProduto = Console.ReadLine();

        Console.WriteLine("Digite o preço do produto:");
        decimal precoProduto = decimal.Parse(Console.ReadLine());

        Console.WriteLine("Digite a quantidade em estoque:");
        int quantidadeEstoque = int.Parse(Console.ReadLine());

        if (ValidacaoProdutos.ValidarProduto(nomeProduto, precoProduto, quantidadeEstoque))
        {
            Estoque produto = new Estoque(nomeProduto, precoProduto, quantidadeEstoque);
            produtos.Add(produto);

            Console.WriteLine("Produto cadastrado com sucesso!");
        }
        else
        {
            Console.WriteLine("Produto não cadastrado.");
        }

        Console.WriteLine("\nProdutos no estoque:");
        foreach (var p in produtos)
        {
            Console.WriteLine($"{p.NomeProduto} - R$ {p.PrecoProduto} - Qtd: {p.QuantidadeEstoque}");
        }
    }
}
