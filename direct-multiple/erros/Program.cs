using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Newtonsoft.Json;

using Model;
using Infra;

var factory = new ConnectionFactory() { 
    HostName = "164.92.158.32",
    UserName = "admin",
    Password = "admin"
    };

using(var connection = factory.CreateConnection()) 
using(var channel = connection.CreateModel())
{
    var EventoDeSalvarErro =  new EventingBasicConsumer(channel);

     EventoDeSalvarErro.Received += (model, ea) =>
     {
        var body = ea.Body.ToArray();
        var stringJson = Encoding.UTF8.GetString(body);
        var erro = JsonConvert.DeserializeObject<ErroGeral>(stringJson) ?? new ErroGeral{};

        Console.WriteLine("[Erro geral]:{0}",erro.Mensagem);
     };

    channel.BasicConsume(queue: "ErrosGerais",
                            autoAck: true,
                            consumer: EventoDeSalvarErro);
    
 

    Console.ReadLine();
}