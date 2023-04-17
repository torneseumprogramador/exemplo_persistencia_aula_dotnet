// using System;

// public class Program
// {
//     public static void Main(string[] args)
//     {
//         // Obter informações do produto
//         Console.WriteLine("Digite o nome do produto:");
//         string? nomeProduto = Console.ReadLine();

//         Console.WriteLine("Digite o preço do produto:");
//         double precoProduto = double.Parse(Console.ReadLine() ?? "0");

//         Console.WriteLine("Digite a quantidade em estoque:");
//         int estoqueProduto = int.Parse(Console.ReadLine() ?? "0");

//         // Obter informações da venda
//         Console.WriteLine("Digite o nome do cliente:");
//         string? nomeCliente = Console.ReadLine();

//         Console.WriteLine("Digite a quantidade vendida:");
//         int quantidadeVendida = int.Parse(Console.ReadLine() ?? "0");

//         Console.WriteLine("Digite a forma de pagamento (D para dinheiro, C para cartão):");
//         string formaPagamento = Console.ReadLine() ?? "0";

//         // Verificar se há estoque suficiente
//         if (quantidadeVendida > estoqueProduto)
//         {
//             Console.WriteLine("Não há estoque suficiente para essa venda.");
//             return;
//         }

//         // Calcular o valor total da venda
//         double valorTotal = quantidadeVendida * precoProduto;

//         // Calcular o troco (se houver) para pagamentos em dinheiro
//         double valorPago = 0;
//         double troco = 0;
//         if (formaPagamento == "D")
//         {
//             Console.WriteLine("Digite o valor pago pelo cliente:");
//             valorPago = double.Parse(Console.ReadLine() ??  "0");

//             if (valorPago < valorTotal)
//             {
//                 Console.WriteLine("O valor pago é insuficiente.");
//                 return;
//             }

//             troco = valorPago - valorTotal;
//         }
//         else if (formaPagamento != "C")
//         {
//             Console.WriteLine("Forma de pagamento inválida.");
//             return;
//         }

//         // Atualizar o estoque e exibir as informações da venda
//         estoqueProduto -= quantidadeVendida;
//         Console.WriteLine($"Produto: {nomeProduto} ({precoProduto:C})");
//         Console.WriteLine($"Cliente: {nomeCliente}");
//         Console.WriteLine($"Quantidade vendida: {quantidadeVendida}");
//         Console.WriteLine($"Valor total: {valorTotal:C}");

//         if (formaPagamento == "D")
//         {
//             Console.WriteLine($"Valor pago: {valorPago:C}");
//             Console.WriteLine($"Troco: {troco:C}");
//         }

//         Console.WriteLine($"Estoque restante: {estoqueProduto}");

//         // Aguardar que o usuário pressione uma tecla para encerrar o programa
//         Console.WriteLine("Pressione qualquer tecla para encerrar o programa...");
//         Console.ReadKey();
//     }
// }
