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
    channel.ExchangeDeclare("Fluxo-Publicacao", ExchangeType.Direct);

    channel.QueueDeclare(queue:"ServicoPalavras",
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

    channel.QueueBind(queue: "ServicoPalavras",
                        exchange: "Fluxo-Publicacao",
                        routingKey: "ServicoPalavras");
                        
    channel.QueueDeclare(queue:"ServicoLetras",
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

    channel.QueueBind(queue: "ServicoLetras",
                        exchange: "Fluxo-Publicacao",
                        routingKey: "ServicoLetras");
          

    channel.QueueDeclare(queue:"ServicoSentimentos",
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

    channel.QueueBind(queue: "ServicoSentimentos",
                        exchange: "Fluxo-Publicacao",
                        routingKey: "ServicoSentimentos");

    channel.QueueDeclare(queue:"ServicoSalvaDB",
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);
                        
    channel.QueueBind(queue: "ServicoSalvaDB",
                        exchange: "Fluxo-Publicacao",
                        routingKey: "ServicoSalvaDB");
                        
}