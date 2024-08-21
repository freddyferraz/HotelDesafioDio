using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using DesafioProjetoHospedagem.Models;

Console.OutputEncoding = Encoding.UTF8;

// Cria os modelos de hóspedes e cadastra na lista de hóspedes
List<Pessoa> hospedes = new List<Pessoa>();
List<Suite> suites = new List<Suite>();
Reserva reserva = new Reserva();

bool executarPrograma = true;


while(executarPrograma)
{
    Console.WriteLine("Escolha uma opção: ");
    Console.WriteLine("1 - Cadastrar Suíte");
    Console.WriteLine("2 - Cadastrar Hóspede");
    Console.WriteLine("3 - Cadastrar Reserva");
    Console.WriteLine("4 - Obter Quantidade de Hóspedes");
    Console.WriteLine("5 - Calcular Valor Diária ");
    Console.WriteLine("0 - Sair");

    int opcaoEscolhida = int.Parse(Console.ReadLine());

    switch (opcaoEscolhida)
    {
        case 1:
            CadastrarSuite();
            break;
        case 2:
            CadastrarHospede();
            break;
        case 3:
            RealizarReserva();
            break;
        case 4:
            Console.WriteLine($"Quantidade de Hóspedes: {reserva.ObterQuantidadeHospedes()}");
            foreach (var item in hospedes)
            {
                Console.WriteLine(item.NomeCompleto);
            }
            break;
        case 5:
            Console.WriteLine($"Valor diária: {reserva.CalcularValorDiaria()}");
            break;
        case 0:
            Console.WriteLine("Obrigado por utilizar os nossos serviços. Volte Sempre ");
            executarPrograma = false;
            break;
        default:
            Console.WriteLine("Opção inválida");
            break;

    }

}


void CadastrarHospede()
{

    Console.WriteLine("Informe a quantidade de hóspedes a serem registrados: ");
    int contador = int.Parse(Console.ReadLine());

    for (int i = 0; i < contador; i++)
    {
        Console.WriteLine($"Informe o nome do {i+1}º Hóspede: ");
        string nomeHospede = Console.ReadLine();
        Console.WriteLine($"Informe o sobrenome do {i + 1}º Hóspede: ");
        string sobrenomeHospede = Console.ReadLine();

        Pessoa p = new Pessoa(nome: nomeHospede, sobrenome: sobrenomeHospede);

        hospedes.Add(p);
    }

   
}

void CadastrarSuite()
{
   
    Console.WriteLine("Informe o Tipo da Suíte a ser cadastrada:");
    string tipoSuite = Console.ReadLine();
    Console.WriteLine("Informe a capacidade total de hóspedes na suíte:");
    int capacidade = int.Parse(Console.ReadLine());
    Console.WriteLine("Informe o valor da diária: ");
    decimal valorDiaria = decimal.Parse(Console.ReadLine());

    Suite suite = new Suite(tipoSuite,capacidade,valorDiaria);
    suites.Add(suite);
}

void RealizarReserva()
{
    int contador = 0;
    Console.WriteLine("Informe a quantidade de dias para a reserva:");
    int diasReservados = int.Parse(Console.ReadLine());
    reserva = new Reserva(diasReservados);
    Console.WriteLine("Informe a suíte desejada:");
    foreach (var item in suites)
    {
        contador++;
        Console.WriteLine($"{contador} - Tipo de Suíte: {item.TipoSuite} - Capacidade: {item.Capacidade} - Valor diária: {item.ValorDiaria}");
    }
    int indexSuite = int.Parse(Console.ReadLine()) -1;

    Suite suite = suites[indexSuite];
    reserva.CadastrarSuite(suite);

    try
    {
        reserva.CadastrarHospedes(hospedes);
    }
    catch (CapacidadeExcedidaException ex)
    {
        Console.WriteLine("Erro: " + ex.Message);
        executarPrograma = false;

    }
}

