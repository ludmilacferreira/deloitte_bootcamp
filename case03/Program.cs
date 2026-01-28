using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        List<Estoque> produtos = new List<Estoque>();

        while (true)
        {
            Console.WriteLine("\n=== MENU ===");
            Console.WriteLine("1 - Adicionar produto");
            Console.WriteLine("2 - Editar produto");
            Console.WriteLine("3 - Remover produto");
            Console.WriteLine("4 - Listar produtos");
            Console.WriteLine("5 - Sair");
            Console.Write("Escolha uma opção: ");

            string opc = Console.ReadLine();

            if (opc == "1")
            {
                AdicionarProduto(produtos);
            }
            else if (opc == "2")
            {
                EditarProduto(produtos);
            }
            else if (opc == "3")
            {
                RemoverProduto(produtos);
            }
            else if (opc == "4")
            {
                ListarProdutos(produtos);
            }
            else if (opc == "5")
            {
                break;
            }
            else
            {
                Console.WriteLine("Opção inválida. Tente novamente.");
            }
        }
    }

    static void AdicionarProduto(List<Estoque> produtos)
    {
        Console.Write("Nome do produto: ");
        string nome = Console.ReadLine();

        Console.Write("Preço do produto: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal preco))
        {
            Console.WriteLine("Preço inválido.");
            return;
        }

        Console.Write("Quantidade em estoque: ");
        if (!int.TryParse(Console.ReadLine(), out int qtd))
        {
            Console.WriteLine("Quantidade inválida.");
            return;
        }

        if (ValidacaoProdutos.ValidarProduto(nome, preco, qtd))
        {
            produtos.Add(new Estoque(nome, preco, qtd));
            Console.WriteLine("Produto cadastrado com sucesso!");
        }
        else
        {
            Console.WriteLine("Produto não cadastrado.");
        }
    }

    static void EditarProduto(List<Estoque> produtos)
    {
        if (produtos.Count == 0)
        {
            Console.WriteLine("Nenhum produto cadastrado.");
            return;
        }

        ListarProdutos(produtos);
        Console.Write("Digite o número do produto a editar: ");
        if (!int.TryParse(Console.ReadLine(), out int idx) || idx < 1 || idx > produtos.Count)
        {
            Console.WriteLine("Índice inválido.");
            return;
        }

        var p = produtos[idx - 1];

        Console.Write($"Novo nome (enter para manter '{p.NomeProduto}'): ");
        string novoNome = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(novoNome)) novoNome = p.NomeProduto;

        Console.Write($"Novo preço (enter para manter {p.PrecoProduto}): ");
        string precoInput = Console.ReadLine();
        decimal novoPreco = p.PrecoProduto;
        if (!string.IsNullOrWhiteSpace(precoInput))
        {
            if (!decimal.TryParse(precoInput, out novoPreco))
            {
                Console.WriteLine("Preço inválido. Edição cancelada.");
                return;
            }
        }

        Console.Write($"Nova quantidade (enter para manter {p.QuantidadeEstoque}): ");
        string qtdInput = Console.ReadLine();
        int novaQtd = p.QuantidadeEstoque;
        if (!string.IsNullOrWhiteSpace(qtdInput))
        {
            if (!int.TryParse(qtdInput, out novaQtd))
            {
                Console.WriteLine("Quantidade inválida. Edição cancelada.");
                return;
            }
        }

        if (ValidacaoProdutos.ValidarProduto(novoNome, novoPreco, novaQtd))
        {
            p.NomeProduto = novoNome;
            p.PrecoProduto = novoPreco;
            p.QuantidadeEstoque = novaQtd;
            Console.WriteLine("Produto atualizado com sucesso.");
        }
        else
        {
            Console.WriteLine("Atualização inválida. Alterações não salvas.");
        }
    }

    static void RemoverProduto(List<Estoque> produtos)
    {
        if (produtos.Count == 0)
        {
            Console.WriteLine("Nenhum produto cadastrado.");
            return;
        }

        ListarProdutos(produtos);
        Console.Write("Digite o número do produto a remover: ");
        if (!int.TryParse(Console.ReadLine(), out int idx) || idx < 1 || idx > produtos.Count)
        {
            Console.WriteLine("Índice inválido.");
            return;
        }

        var removido = produtos[idx - 1];
        produtos.RemoveAt(idx - 1);
        Console.WriteLine($"Produto '{removido.NomeProduto}' removido com sucesso.");
    }

    static void ListarProdutos(List<Estoque> produtos)
    {
        if (produtos.Count == 0)
        {
            Console.WriteLine("Nenhum produto no estoque.");
            return;
        }

        Console.WriteLine("\nProdutos no estoque:");
        for (int i = 0; i < produtos.Count; i++)
        {
            var p = produtos[i];
            Console.WriteLine($"{i + 1}. {p.NomeProduto} - R$ {p.PrecoProduto} - Qtd: {p.QuantidadeEstoque}");
        }
    }
}



