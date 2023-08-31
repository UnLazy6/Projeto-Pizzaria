using Projeto.Models;
using System;
using System.Collections.Generic;

List<Pizza> listaDePizzas = new List<Pizza>();

bool continuar = true;

while (continuar){
    Console.WriteLine("Bem-vindo ao Projeto Pizzaria");
    Console.WriteLine("Escolha uma opção:");
    Console.WriteLine("1 - Criar pizza");
    Console.WriteLine("2 - Listar pizzas");
    Console.WriteLine("3 - Sair do programa");

    Console.WriteLine("Digite sua opção: ");
    var opcao = Console.ReadLine();

    switch(opcao){
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
            
            foreach(var p in listaDePizzas){
                Console.WriteLine($"Nome: {p.Nome}");
                Console.WriteLine($"Ingredientes: {p.Ingredientes}");
                Console.WriteLine($"Preço: {p.Preço}");
                Console.WriteLine("\n");
            }
            break;

        case "3":
            continuar = false;
            Console.WriteLine("\nSaindo...");
            break;

        default:
            Console.WriteLine("\nOpção Inválida. Digite novamente.");
            break;
    }
}
