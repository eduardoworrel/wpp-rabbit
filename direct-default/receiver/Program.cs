using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Newtonsoft.Json;
using model;

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
         var stringJson = Encoding.UTF8.GetString(body);
         var mensagem = JsonConvert.DeserializeObject<Mensagem>(stringJson);
         Console.WriteLine("[{0}]: {1} - {2}", mensagem.Autor, mensagem.Conteudo, mensagem.CreatedAt);
  
     };

    channel.BasicConsume(queue: "sozinho",
                            autoAck: true,
                            consumer: evento);

    Console.ReadLine();
}