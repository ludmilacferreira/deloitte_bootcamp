using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        var manager = new VisitorManager();
        while (true)
        {
            try
            {
                Console.WriteLine("\n--- Menu de Visitantes ---");
                Console.WriteLine("1 - Cadastrar visitante");
                Console.WriteLine("2 - Listar visitantes");
                Console.WriteLine("3 - Buscar por nome");
                Console.WriteLine("4 - Registrar saída (por documento)");
                Console.WriteLine("5 - Listar primeiras visitas");
                Console.WriteLine("6 - Ordenar visitantes");
                Console.WriteLine("0 - Sair");
                Console.Write("Escolha uma opção: ");
                var opt = Console.ReadLine();

                switch (opt)
                {
                    case "1":
                        Cadastrar(manager);
                        break;
                    case "2":
                        Listar(manager.ListAll());
                        break;
                    case "3":
                        Buscar(manager);
                        break;
                    case "4":
                        RegistrarSaida(manager);
                        break;
                    case "5":
                        Listar(manager.FirstTimeVisitors());
                        break;
                    case "6":
                        Ordenar(manager);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }
    }

    static void Cadastrar(VisitorManager manager)
    {
        Console.Write("Nome: ");
        var nome = Console.ReadLine();
        Console.Write("Documento: ");
        var doc = Console.ReadLine();
        Console.Write("É primeira vez? (s/n): ");
        var prima = Console.ReadLine();
        var v = new Visitante
        {
            Nome = nome,
            Documento = doc,
            HorarioChegada = DateTime.Now,
            EPrimaVez = prima?.ToLower() == "s"
        };
        manager.Add(v);
        Console.WriteLine("Visitante cadastrado.");
    }

    static void Listar(IEnumerable<Visitante> lista)
    {
        foreach (var v in lista)
        {
            Console.WriteLine(v);
        }
    }

    static void Buscar(VisitorManager manager)
    {
        Console.Write("Nome para buscar: ");
        var nome = Console.ReadLine();
        var encontrados = manager.FindByName(nome);
        Listar(encontrados);
    }

    static void RegistrarSaida(VisitorManager manager)
    {
        Console.Write("Documento do visitante para registrar saída: ");
        var doc = Console.ReadLine();
        var ok = manager.RegisterExit(doc);
        Console.WriteLine(ok ? "Saída registrada." : "Visitante não encontrado ou já saiu.");
    }

    static void Ordenar(VisitorManager manager)
    {
        Console.WriteLine("1 - Ordenar por nome\n2 - Ordenar por chegada");
        var o = Console.ReadLine();
        switch (o)
        {
            case "1":
                Listar(manager.SortedByName());
                break;
            case "2":
                Listar(manager.SortedByArrival());
                break;
            default:
                Console.WriteLine("Opção inválida.");
                break;
        }
    }
}
