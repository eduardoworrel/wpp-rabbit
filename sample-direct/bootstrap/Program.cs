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
  channel.ExchangeDeclare("ew-FalaComigoDev",ExchangeType.Direct);
  
  channel.QueueDeclare(queue:"ew-chat",
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

  channel.QueueBind(queue: "ew-chat",
            exchange: "ew-FalaComigoDev",
            routingKey: "ew-chat");
}