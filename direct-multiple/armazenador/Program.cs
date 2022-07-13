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
    var EventoDeSalvarUsuarioNormal =  new EventingBasicConsumer(channel);
    var EventoDeSalvarUsuarioAdmin =  new EventingBasicConsumer(channel);

     EventoDeSalvarUsuarioNormal.Received += (model, ea) =>
     {
        var body = ea.Body.ToArray();
        var stringJson = Encoding.UTF8.GetString(body);
        var usuarioNormal = JsonConvert.DeserializeObject<UsuarioNormal>(stringJson) ?? new UsuarioNormal{};

        using (var db = new MyDbContext())
        {
            db.UsuarioNormal.Add(usuarioNormal);
            db.SaveChanges();
            Console.WriteLine("deu bom");
        }
     };

     EventoDeSalvarUsuarioAdmin.Received += (model, ea) =>
     {
        var body = ea.Body.ToArray();
        var stringJson = Encoding.UTF8.GetString(body);
        var usuarioAdmin = JsonConvert.DeserializeObject<UsuarioAdmin>(stringJson) ?? new UsuarioAdmin{};
        try{
            using (var db = new MyDbContext())
            {
                db.UsuarioAdmin.Add(usuarioAdmin);
                db.SaveChanges();
                Console.WriteLine("deu bom pro admin tbm");
            }
        }catch(Exception e){
            var erro =  new ErroGeral{
                Mensagem = e.Message
            };
            var corpo = JsonConvert.SerializeObject(erro);
            var usuarioBytes = Encoding.UTF8.GetBytes(corpo);

            channel.BasicPublish(exchange: "Erros",
                            routingKey: "",
                            basicProperties: null,
                            body: usuarioBytes);
        }
       
     };

    channel.BasicConsume(queue: "usuarioNormal",
                            autoAck: true,
                            consumer: EventoDeSalvarUsuarioNormal);
    
    channel.BasicConsume(queue: "usuarioAdmin",
                            autoAck: true,
                            consumer: EventoDeSalvarUsuarioAdmin);

    Console.ReadLine();
}