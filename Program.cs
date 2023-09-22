using Projeto.Models;
using System;
using System.Collections.Generic;
using System.Linq;

List<Pizza> listaDePizzas = new List<Pizza>();
List<Cliente> listaDeClientes = new List<Cliente>();
List<Pedido> listaDePedidos = new List<Pedido>();
List<Pedido> pedidos = new List<Pedido>();

bool continuar = true;

while (continuar)
{
    Console.WriteLine("Bem-vindo ao Projeto Pizzaria");
    Console.WriteLine("Escolha uma opção:");
    Console.WriteLine("1 - Criar pizza");
    Console.WriteLine("2 - Listar pizzas");
    Console.WriteLine("3 - Criar novo pedido");
    Console.WriteLine("4 - Listar pedidos");
    Console.WriteLine("5 - Pagar pedidos");
    Console.WriteLine("6 - Sair do programa");

    Console.WriteLine("Digite sua opção: ");
    var opcao = Console.ReadLine();

    switch (opcao)
    {
        case "1":
            var pizza = new Pizza();

            Console.WriteLine("\nCriar uma pizza!");
            Console.WriteLine("Escolha um nome para sua Pizza: ");
            var nome = Console.ReadLine();

            Console.WriteLine("Digite os ingredientes da Pizza separados por vírgula: ");
            var ingredientes = Console.ReadLine();

            Console.WriteLine("Digite o preço da Pizza (formato 00,00): ");
            var preço = Console.ReadLine();

            pizza.Nome = nome;
            pizza.Ingredientes = ingredientes;
            pizza.Preço = preço;

            listaDePizzas.Add(pizza);
            Console.WriteLine("Pizza criada e adicionada à lista!\n");
            break;

        case "2":
            Console.WriteLine("\nListar as Pizzas!");

            foreach (var p in listaDePizzas)
            {
                Console.WriteLine($"Nome: {p.Nome}");
                Console.WriteLine($"Ingredientes: {p.Ingredientes}");
                Console.WriteLine($"Preço: {p.Preço:C}");
                Console.WriteLine("\n");
            }
            break;

        case "3":
            Console.WriteLine("Criar um novo pedido!");
            Console.WriteLine("Digite o nome do cliente: ");
            var nomeCliente = Console.ReadLine();

            Console.WriteLine("Digite o telefone do cliente: ");
            var telefoneCliente = Console.ReadLine();

            Cliente clienteExistente = listaDeClientes.Find(c => c.Nome.Equals(nomeCliente, StringComparison.OrdinalIgnoreCase) && c.Telefone.Equals(telefoneCliente));

            if (clienteExistente == null)
            {
                Console.WriteLine("Cliente não encontrado. Criando novo cliente...");

                var novoCliente = new Cliente
                {
                    Nome = nomeCliente,
                    Telefone = telefoneCliente
                };

                clienteExistente = novoCliente;
                listaDeClientes.Add(novoCliente);
            }

            var novoPedido = new Pedido
            {
                Cliente = clienteExistente,
                Pizzas = new List<Pizza>(), // Use a lista vazia aqui
                ValorTotal = 0.0
            };

            bool continuarAdicionando = true;

            while (continuarAdicionando)
            {
                Console.WriteLine("Escolha uma pizza da lista:");

                foreach (var item in listaDePizzas)
                {
                    Console.WriteLine(item);
                };

                Console.WriteLine("Digite o nome da pizza escolhida:");
                var nomePizzaEscolhida = Console.ReadLine();

                Pizza pizzaEscolhida = listaDePizzas.Find(p => p.Nome.Equals(nomePizzaEscolhida, StringComparison.OrdinalIgnoreCase));

                if (pizzaEscolhida != null)
                {
                    novoPedido.Pizzas.Add(pizzaEscolhida);
                    Console.WriteLine("Pizza adicionada ao pedido!");

                    novoPedido.ValorTotal += Convert.ToDouble(pizzaEscolhida.Preço);
                }
                else
                {
                    Console.WriteLine("Pizza não encontrada. Pedido não adicionado.");
                }

                Console.WriteLine("Deseja adicionar outra pizza? (S/N)");
                string resposta = Console.ReadLine();
                continuarAdicionando = resposta.Equals("S", StringComparison.OrdinalIgnoreCase);
            }

            listaDePedidos.Add(novoPedido);
            Console.WriteLine("Pedido criado e adicionado à lista!");
            Console.WriteLine($"Valor total do pedido: {novoPedido.ValorTotal:C}");
            break;

        case "4":
            Console.WriteLine("Listagem de Pedidos:");

            foreach (var pedido in listaDePedidos)
            {
                Console.WriteLine($"Número do Pedido: {pedido.Numero}");
                Console.WriteLine($"Cliente: {pedido.Cliente.Nome} ({pedido.Cliente.Telefone})");
                Console.WriteLine("Pizzas:");

                foreach (var pizzaa in pedido.Pizzas)
                {
                    Console.WriteLine($"- {pizzaa.Nome} ({pizzaa.Preço:C})");
                }

                Console.WriteLine($"Valor Total: {pedido.ValorTotal:C}");
                Console.WriteLine($"Pedido Pago: {pedido.Pago}");
                Console.WriteLine($"Falta Pagar: {pedido.FaltaPagar:C}");
                Console.WriteLine();
            }
            break;

        case "5":
            Console.WriteLine("Pagar Pedidos:");

            Console.WriteLine("Digite o número do pedido que deseja pagar: ");
            if (int.TryParse(Console.ReadLine(), out int numeroPedido))
            {
                // Encontre o pedido pelo número (supondo que cada pedido tenha um número único)
                Pedido pedidoParaPagar = listaDePedidos.FirstOrDefault(p => p.Numero == numeroPedido);

                if (pedidoParaPagar != null)
                {
                    Console.WriteLine($"Total do Pedido: {pedidoParaPagar.ValorTotal:C}");

                    Console.WriteLine("Escolha a forma de pagamento (dinheiro, cartão de débito, vale-refeição): ");
                    string formaDePagamento = Console.ReadLine();

                    Console.WriteLine("Digite o valor pago: ");
                    if (double.TryParse(Console.ReadLine(), out double valorPago))
                    {
                        pedidoParaPagar.FormaDePagamento = formaDePagamento;
                        pedidoParaPagar.ValorPago = valorPago;

                        // Calcule o valor que ainda falta pagar
                        double faltaPagar = pedidoParaPagar.ValorTotal - pedidoParaPagar.ValorPago;
                        pedidoParaPagar.FaltaPagar = faltaPagar;

                        // Calcule o troco, se houver
                        double troco = pedidoParaPagar.ValorPago - pedidoParaPagar.ValorTotal;
                        pedidoParaPagar.Troco = troco;

                        // Defina o pedido como pago se o valor pago for igual ou superior ao valor total
                        if (pedidoParaPagar.ValorPago >= pedidoParaPagar.ValorTotal)
                        {
                            pedidoParaPagar.Pago = true;
                            pedidoParaPagar.FaltaPagar = 0.0;
                        }

                        Console.WriteLine($"Pagamento registrado com sucesso.");
                        Console.WriteLine($"Forma de pagamento: {pedidoParaPagar.FormaDePagamento}");
                        Console.WriteLine($"Valor pago: {pedidoParaPagar.ValorPago:C}");
                        Console.WriteLine($"Falta pagar: {pedidoParaPagar.FaltaPagar:C}");
                        Console.WriteLine($"Troco: {pedidoParaPagar.Troco:C}");
                    }
                    else
                    {
                        Console.WriteLine("Valor pago inválido.");
                    }
                }
                else
                {
                    Console.WriteLine("Pedido não encontrado.");
                }
            }
            else
            {
                Console.WriteLine("Número de pedido inválido.");
            }
            break;

        case "6":
            continuar = false;
            Console.WriteLine("\nSaindo...");
            break;

        default:
            Console.WriteLine("\nOpção Inválida. Digite novamente.");
            break;
    }
}
