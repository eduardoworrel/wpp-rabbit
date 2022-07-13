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
  channel.ExchangeDeclare("salvaUsuario", ExchangeType.Direct);
  
  channel.QueueDeclare(queue:"usuarioNormal",
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

  channel.QueueDeclare(queue:"usuarioAdmin",
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

  channel.QueueBind(queue: "usuarioNormal",
            exchange: "salvaUsuario",
            routingKey: "usuarioNormal");
            
  channel.QueueBind(queue: "usuarioAdmin",
            exchange: "salvaUsuario",
            routingKey: "usuarioAdmin");

  channel.ExchangeDeclare("Erros", ExchangeType.Fanout);
  
  channel.QueueDeclare(queue:"ErrosGerais",
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

  channel.QueueBind(queue: "ErrosGerais",
            exchange: "Erros",
            routingKey: "");

}