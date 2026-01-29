using System;
using System.Collections.Generic;
using System.Linq;

public class VisitorManager
{
    private readonly List<Visitante> visitantes = new List<Visitante>();

    public void Add(Visitante v)
    {
        visitantes.Add(v);
    }

    public IEnumerable<Visitante> ListAll() => visitantes;

    public IEnumerable<Visitante> FindByName(string nome)
    {
        return visitantes.Where(v => v.Nome != null && v.Nome.IndexOf(nome, StringComparison.OrdinalIgnoreCase) >= 0);
    }

    public bool RegisterExit(string documento)
    {
        var v = visitantes.FirstOrDefault(x => x.Documento == documento && !x.HorarioSaida.HasValue);
        if (v == null) return false;
        v.HorarioSaida = DateTime.Now;
        return true;
    }

    public IEnumerable<Visitante> FirstTimeVisitors()
    {
        return visitantes.Where(v => v.EPrimaVez);
    }

    public IEnumerable<Visitante> SortedByName()
    {
        return visitantes.OrderBy(v => v.Nome);
    }

    public IEnumerable<Visitante> SortedByArrival()
    {
        return visitantes.OrderBy(v => v.HorarioChegada);
    }
}
