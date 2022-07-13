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
     var evento =  new EventingBasicConsumer(channel);

     evento.Received += (model, ea) =>
     {   
         var body = ea.Body.ToArray();
         var stringJson = Encoding.UTF8.GetString(body);
         var mensagem = JsonConvert.DeserializeObject<Mensagem>(stringJson);
         Console.WriteLine("[{0}]: {1} - {2}", mensagem.Autor, mensagem.Conteudo, mensagem.CreatedAt);
     };

    channel.BasicConsume(queue: "ew-chat",
                            autoAck: true,
                            consumer: evento);
    

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
            channel.BasicPublish(exchange: "ew-FalaComigoDev",
                            routingKey: "ew-chat",
                            basicProperties: null,
                            body: Encoding.UTF8.GetBytes(body));
        }
        
    }   
}
    
