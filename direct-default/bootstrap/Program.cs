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

  channel.QueueDeclare(queue:"sozinho",
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

}