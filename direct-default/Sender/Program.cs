using model;
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

    Console.WriteLine("Qual seu nome?");
     var nome = Console.ReadLine() ?? "";
    Console.WriteLine("Olá {0} Envie uma mensagem ou digite /sair para sair",nome);

    var continua = true;
    
    while(continua){
        
        Console.WriteLine("Nova Mensagem:");
        var mensagem = Console.ReadLine() ?? "";
        if(mensagem == "/sair"){
            continua = false;
        }else{
            var corpoDaMensagem = new Mensagem{
                Autor = nome,
                Conteudo = mensagem,
                CreatedAt = DateTime.Now
            };
            var body = JsonConvert.SerializeObject(corpoDaMensagem);


            channel.BasicPublish(exchange: "",
                            routingKey: "sozinho",
                            basicProperties: null,
                            body: Encoding.UTF8.GetBytes(body));
        }
        
    }   
}
    
