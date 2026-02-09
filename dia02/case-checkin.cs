using System;
using System.Collections.Generic;
using System.Linq;


class Program
{
    static List<Visitante> visitantes = new();
    static int nextId = 1;


    static void Main()
    {
        bool executando = true;


        while (executando)
        {
            Console.WriteLine("Check-in de Visitantes");
            Console.WriteLine("1 - Cadastrar visitante");
            Console.WriteLine("2 - Listar visitantes");
            Console.WriteLine("3 - Buscar visitante pelo nome");
            Console.WriteLine("4 - Registrar saída do visitante");
            Console.WriteLine("5 - Listar visitantes de primeira visita");
            Console.WriteLine("6 - Listar visitantes ordenados por ID");
            Console.WriteLine("0 - Sair");
            Console.Write("Opção: ");


            try
            {
                int opcao = int.Parse(Console.ReadLine());


                switch (opcao)
                {
                    case 1:
                        Cadastrar();
                        break;


                    case 2:
                        Listar();
                        break;


                    case 3:
                        BuscarPorNome();
                        break;


                    case 4:
                        RegistrarSaida();
                        break;


                    case 5:
                        ListarPrimeiraVisita();
                        break;


                    case 6:
                        ListarOrdenadoPorId();
                        break;


                    case 0:
                        executando = false;
                        break;


                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }
    }




    static void Cadastrar()
    {
        Console.Write("Nome: ");
        string nome = Console.ReadLine();


        Console.Write("Documento: ");
        string documento = Console.ReadLine();


        Console.Write("É primeira visita? (s/n): ");
        bool primeiraVez = Console.ReadLine()?.ToLower() == "s";


        Visitante visitante = new Visitante
        {
            Id = nextId++,
            Nome = nome,
            Documento = documento,
            HorarioChegada = DateTime.Now,
            EPrimeiraVez = primeiraVez
        };


        visitantes.Add(visitante);
        Console.WriteLine("Visitante cadastrado com sucesso!");
    }


    static void Listar()
    {
        if (!visitantes.Any())
        {
            Console.WriteLine("Nenhum visitante cadastrado.");
            return;
        }


        foreach (var v in visitantes)
            Console.WriteLine(v);
    }


    static void BuscarPorNome()
    {
        Console.Write("Digite o nome: ");
        string nome = Console.ReadLine();


        var resultado = visitantes.Where(v =>
            v.Nome != null &&
            v.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase));


        if (!resultado.Any())
        {
            Console.WriteLine("Nenhum visitante encontrado.");
            return;
        }


        foreach (var v in resultado)
            Console.WriteLine(v);
    }


    static void RegistrarSaida()
    {
        Console.Write("Documento do visitante: ");
        string documento = Console.ReadLine();


        var visitante = visitantes.FirstOrDefault(v =>
            v.Documento == documento && !v.HorarioSaida.HasValue);


        if (visitante == null)
        {
            Console.WriteLine("Visitante não encontrado ou saída já registrada.");
            return;
        }


        visitante.HorarioSaida = DateTime.Now;
        Console.WriteLine("Saída registrada com sucesso!");
    }


    static void ListarPrimeiraVisita()
    {
        var primeiraVisita = visitantes.Where(v => v.EPrimeiraVez);


        if (!primeiraVisita.Any())
        {
            Console.WriteLine("Nenhum visitante de primeira visita.");
            return;
        }


        foreach (var v in primeiraVisita)
            Console.WriteLine(v);
    }


    static void ListarOrdenadoPorId()
    {
        foreach (var v in visitantes.OrderBy(v => v.Id))
            Console.WriteLine(v);
    }
}




class Visitante
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Documento { get; set; }
    public DateTime HorarioChegada { get; set; }
    public bool EPrimeiraVez { get; set; }
    public DateTime? HorarioSaida { get; set; }


    public override string ToString()
    {
        string saida = HorarioSaida.HasValue
            ? HorarioSaida.Value.ToString("g")
            : "--";


        return $"ID: {Id} | Nome: {Nome} | Documento: {Documento} | Chegada: {HorarioChegada:g} | Saída: {saida} | Primeira vez: {EPrimeiraVez}";
    }
}



