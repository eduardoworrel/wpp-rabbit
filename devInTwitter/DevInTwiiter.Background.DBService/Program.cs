using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Newtonsoft.Json;

using Models;
using System.Net.Http.Json;
using Data;

var factory = new ConnectionFactory()
{
    HostName = "164.92.158.32",
    UserName = "admin",
    Password = "admin"
};

using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())
{
 var eventoProcessaSentimentoDoPost = new EventingBasicConsumer(channel);


    eventoProcessaSentimentoDoPost.Received += async (model, ea) =>
    {
        var body = ea.Body.ToArray();
        var stringJson = Encoding.UTF8.GetString(body);
        var post = JsonConvert.DeserializeObject<Post>(stringJson) ?? new Post { };

        var ctx = new ApplicationDbContext();
        ctx.Post.Add(Post);
        ctx.SaveChanges();

    };


    channel.BasicConsume(queue: "ServicoSentimentos",
                            autoAck: true,
                            consumer: eventoProcessaSentimentoDoPost);

}