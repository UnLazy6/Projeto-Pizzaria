using System.ComponentModel;

namespace Projeto.Models;

public class Pedido{
    public int Numero { get; set; }
    public Cliente Cliente { get; set; }
    public List<Pizza> Pizzas { get; set; }
    public double ValorTotal { get; set; }
    public string FormaDePagamento { get; set; } 
    public double ValorPago { get; set; }
    public double FaltaPagar { get; set; }
    public double Troco { get; set; }
    public bool Pago { get; set; }
}