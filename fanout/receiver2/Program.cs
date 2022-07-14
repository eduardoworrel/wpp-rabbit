using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Newtonsoft.Json;

var factory = new ConnectionFactory() { 
    HostName = "164.92.158.32",
    UserName = "admin",
    Password = "admin"
    };

using(var connection = factory.CreateConnection()) 
using(var channel = connection.CreateModel())
{
    var evento =  new EventingBasicConsumer(channel);

     evento.Received += (model, ea) =>
     {
        var body = ea.Body.ToArray();
        var str = Encoding.UTF8.GetString(body);
        Console.WriteLine("[B]: {0}", str);
     };

    channel.BasicConsume(queue: "b",
                            autoAck: true,
                            consumer: evento);
                            
    Console.WriteLine("~ Fila a sendo consumida:");
    Console.ReadLine();
}