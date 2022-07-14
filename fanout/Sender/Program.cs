
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;


var factory = new ConnectionFactory()
{
HostName = "164.92.158.32",
UserName = "admin",
Password = "admin"
};

using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())
{


    var continua = true;
    
    while(continua){
        
        Console.WriteLine("Nova Mensagem:");
        var mensagem = Console.ReadLine() ?? "";
        if(mensagem == "/sair"){
            continua = false;
        }else{
            channel.BasicPublish(exchange: "all",
                            routingKey: "",
                            basicProperties: null,
                            body: Encoding.UTF8.GetBytes(mensagem));
        }
        
    }   
}
    
