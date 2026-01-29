using System;

public class Visitante
{
    public string Nome { get; set; }
    public string Documento { get; set; }
    public DateTime HorarioChegada { get; set; }
    public bool EPrimaVez { get; set; }
    public DateTime? HorarioSaida { get; set; }

    public override string ToString()
    {
        var saida = HorarioSaida.HasValue ? HorarioSaida.Value.ToString("g") : "--";
        return $"Nome: {Nome}, Documento: {Documento}, Chegada: {HorarioChegada:g}, Saída: {saida}, PrimeiraVez: {EPrimaVez}";
    }
}
