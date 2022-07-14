using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory() { 
    HostName = "164.92.158.32",
    UserName = "admin",
    Password = "admin"
    };

using(var connection = factory.CreateConnection()) 
using(var channel = connection.CreateModel())
{
  channel.ExchangeDeclare("corridaDeAutomoveis",ExchangeType.Topic);

  channel.QueueDeclare(queue:"veiculosAutomotivos",
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

  channel.QueueDeclare(queue:"tentativaDeInscricao",
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);
   
  channel.QueueDeclare(queue:"corridaDeCarros",
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);
    
   
  channel.QueueDeclare(queue:"corridaDeMotos",
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);
    
   
  channel.QueueDeclare(queue:"corridaVeiculosAzuis",
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);
    
  
  channel.QueueBind(queue: "veiculosAutomotivos",
            exchange: "corridaDeAutomoveis",
            routingKey: "automotivo.#");

  channel.QueueBind(queue: "tentativaDeInscricao",
            exchange: "corridaDeAutomoveis",
            routingKey: "#");

  channel.QueueBind(queue: "corridaDeCarros",
            exchange: "corridaDeAutomoveis",
            routingKey: "*.carro.*");

  channel.QueueBind(queue: "corridaDeMotos",
            exchange: "corridaDeAutomoveis",
            routingKey: "*.moto.*");

  channel.QueueBind(queue: "corridaVeiculosAzuis",
            exchange: "corridaDeAutomoveis",
            routingKey: "*.*.azul");

}
// automotivo.carro.azul
// naoautomotivo.bicicleta.amarela
//