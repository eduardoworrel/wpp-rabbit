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
  channel.ExchangeDeclare("all",ExchangeType.Fanout);

  channel.QueueDeclare(queue:"a",
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

  channel.QueueBind(queue: "a",
            exchange: "all",
            routingKey: "");

  channel.QueueDeclare(queue:"b",
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);
  
    channel.QueueBind(queue: "b",
            exchange: "all",
            routingKey: "");


  channel.QueueDeclare(queue:"c",
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

  channel.QueueBind(queue: "c",
            exchange: "all",
            routingKey: "");
}