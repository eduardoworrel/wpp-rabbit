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
    var veiculosAutomotivos =  new EventingBasicConsumer(channel);

     veiculosAutomotivos.Received += (model, ea) =>
     {
        var body = ea.Body.ToArray();
        var str = Encoding.UTF8.GetString(body);
        Console.WriteLine("[veiculosAutomotivos]: {0}", str);
     };

    channel.BasicConsume(queue: "veiculosAutomotivos",
                            autoAck: true,
                            consumer: veiculosAutomotivos);





    var tentativaDeInscricao =  new EventingBasicConsumer(channel);

    tentativaDeInscricao.Received += (model, ea) =>
    {
    var body = ea.Body.ToArray();
    var str = Encoding.UTF8.GetString(body);
    Console.WriteLine("[tentativaDeInscricao]: {0}", str);
    };

    channel.BasicConsume(queue: "tentativaDeInscricao",
                            autoAck: true,
                            consumer: tentativaDeInscricao);







    var corridaDeCarros =  new EventingBasicConsumer(channel);

    corridaDeCarros.Received += (model, ea) =>
    {
    var body = ea.Body.ToArray();
    var str = Encoding.UTF8.GetString(body);
    Console.WriteLine("[corridaDeCarros]: {0}", str);
    };

    channel.BasicConsume(queue: "corridaDeCarros",
                            autoAck: true,
                            consumer: corridaDeCarros);



                            

    var corridaDeMotos =  new EventingBasicConsumer(channel);

    corridaDeMotos.Received += (model, ea) =>
    {
    var body = ea.Body.ToArray();
    var str = Encoding.UTF8.GetString(body);
    Console.WriteLine("[corridaDeMotos]: {0}", str);
    };

    channel.BasicConsume(queue: "corridaDeMotos",
                            autoAck: true,
                            consumer: corridaDeMotos);






                            

    var corridaVeiculosAzuis =  new EventingBasicConsumer(channel);

    corridaVeiculosAzuis.Received += (model, ea) =>
    {
    var body = ea.Body.ToArray();
    var str = Encoding.UTF8.GetString(body);
    Console.WriteLine("[corridaVeiculosAzuis]: {0}", str);
    };

    channel.BasicConsume(queue: "corridaVeiculosAzuis",
                            autoAck: true,
                            consumer: corridaVeiculosAzuis);

    Console.WriteLine("~ Fila a sendo consumida:");
    Console.ReadLine();
}