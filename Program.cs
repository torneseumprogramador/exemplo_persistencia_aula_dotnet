﻿// var sair = "1";
// while (sair != "0")
// {
//     Console.WriteLine("Oiiii - " + sair);
//     sair = Console.ReadLine();
// }

// for (var i=10; i < 30; i++)
//     Console.WriteLine(i);


// var sair = "0";
// do
// {
//     Console.WriteLine("Oiiii - " + sair);
//     sair = Console.ReadLine();

// } while (sair != "0");

// string[] lista = new string[]{ "Danilo", "Alexandra", "Liah" };
// foreach (var item in lista)
// {
//     Console.WriteLine(item);
// }

// while (true)
// {
//     Console.WriteLine("Oiiii - ");
//     var sair = Console.ReadLine();
//     if(sair == "0") break;
// }


// while (true)
// {
//     Console.WriteLine("Digite um numero");
//     var numero = Console.ReadLine();
//     if(numero == "0") continue;

//     Console.WriteLine($"Você digitou um numero diferente de 0 {numero}");
// }


// string[] lista1 = new string[3];
// lista1[0] = "primeira";
// lista1[1] = "segunda";
// lista1[2] = "terceira";

// string[] lista2 = new string[]{ "Danilo", "Alexandra", "Liah" };

// List<string> lista3 = new List<string>();
// lista3.Add("Leandro");
// var lista4 = lista3.ToArray();

// foreach(var item in lista3)
// {
//     Console.WriteLine("Item: " + item);
// }



// int x = 0;
// while(x >= 0 && x <= 100) // E
// {
//     Console.WriteLine("digite o valor de x");
//     int.TryParse(Console.ReadLine(), out x);

//     Console.WriteLine("o  novo valor de x é: " + x);
// }


// int x = 0;
// while(!(x >= 0) || x <= 100) // OU
// {
//     Console.WriteLine("digite o valor de x");
//     int.TryParse(Console.ReadLine(), out x);

//     Console.WriteLine("o  novo valor de x é: " + x);
// }


using console_app.Models;
using console_app.Servicos;

while(true)
{
    Console.Clear();
    Console.WriteLine("Digite uma das opções abaixo:");
    Console.WriteLine("1 - Cadastrar Cliente");
    Console.WriteLine("2 - Listar Cliente");
    Console.WriteLine("3 - Sair");

    var opcao = Console.ReadLine();
    var sair = false;

    switch(opcao)
    {
        case "1":
            Console.Clear();
            Console.WriteLine("Digite o nome do cliente");
            var nome = Console.ReadLine();
            Console.WriteLine("Digite o telefone do cliente");
            var telefone = Console.ReadLine();

            Cliente cliente = new Cliente() 
            { 
                Nome = nome,
                Telefone = telefone 
            };

            // var clientes = JsonPersistencia.Lista("clientes.json");
            // clientes.Add(cliente);
            // JsonPersistencia.Salvar("clientes.json", clientes);

            var clientes = CsvPersistencia.Lista("clientes.csv");
            clientes.Add(cliente);
            CsvPersistencia.Salvar("clientes.csv", clientes);

            Console.WriteLine("Cliente cadastrado com sucesso ...");
            Thread.Sleep(1000);
            break;
        case "2":
            Console.WriteLine("=== Lista de clientes =====");
            // var clientesArquivo = JsonPersistencia.Lista("clientes.json");
            var clientesArquivo = CsvPersistencia.Lista("clientes.csv");
            foreach(var cli in clientesArquivo)
            {
                Console.WriteLine($"Nome: {cli.Nome}");
                Console.WriteLine($"Telefone: {cli.Telefone}");
                Console.WriteLine("------------------------------");
            }

            Console.WriteLine("Pressione Enter para continuar ...");
            Console.ReadKey();
            break;
        default:
            sair = true;
            break;
    }

    if(sair) break;
}