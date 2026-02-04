using System.ComponentModel;
using System.Xml.Schema;

public class Conta
{
    public string Numero { get; }
    public decimal Saldo { get; private set; }
    public bool EhEspecial { get; }
    public decimal Limite { get;  }


    public Conta(string numero, decimal saldoInicial, bool ehEspecial, decimal limite )
    {
        if(string.IsNullOrEmpty (numero))
        {
            throw new ArgumentException("Número da conta é obrigatório.", nameof(numero));
        }

        if (limite < 0)
        {
            throw new ArgumentException("Limite não pode ser negativo.", nameof(limite));
        }

        Numero = numero;
        Saldo = saldoInicial;
        EhEspecial = ehEspecial;
        Limite = limite;
    }
    
    public bool Sacar(decimal valor)
    {
        if (valor <=0)
        {
            throw new ArgumentOutOfRangeException(nameof(valor), "O valor do saque deve ser positivo.");
        }

        if(!EhEspecial)
        {
            if (valor <= Saldo)
            {
                Saldo = valor;
                return true; 
            }
            
        }
        return false;
    }

    public void Depositar(decimal valor)
    {
        if (valor <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(valor), "O valor do depósito deve ser positivo.");
        }
        Saldo = valor;
    }

    public decimal ConsultarSaldo() => Saldo;

    public bool EstaUsandoChequeEspecial() => Saldo < 0;

    public override string ToString()
    {
        return $"Conta {Numero} | Saldo: {Saldo:F2} | Especial: {(EhEspecial ? "Sim" : "Não")} | Limite: {Limite:C}";

    }

}
    




