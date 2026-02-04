using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;

{
    Lampada lampada = new Lampada();
    lampada.ligar();
    Console.WriteLine("Estado da lâmpada"+ (lampada.EstaLigada()? " ligada":" desligada"));
    lampada.desligar();
    Console.WriteLine("Estado da lâmpada"+ (lampada.EstaLigada()? " ligada":" desligada"));
  
}

{
    var contaUsuario = new Conta(numero: "0508-X", saldoInicial: 500, ehEspecial: false, limite: 0);
    Console.WriteLine("Conta do Usuario");
    Console.WriteLine(contaUsuario);

    Console.WriteLine("Tentando sacar R$600,00 (deve falhar)");
    bool sacou = contaUsuario.Sacar(400);
    Console.WriteLine($"Saque realizado? {(sacou ? "Sim" : "Não")}. Saldo: {contaUsuario.ConsultarSaldo()}");
}