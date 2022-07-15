using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Newtonsoft.Json;

using Models;
using System.Net.Http.Json;
using ViewModels;

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

        var palavras = post.Conteudo?.Split(" ") ?? new string[] { };

        post.QuantidadeDePalavras = palavras.Length;

        var letras = string.Join("", palavras).Split("");

        post.QuantidadeDeLetras = letras.Length;


        post.Emocao =  Emotion.Angry;
        post.Sentimento = Sentiment.Negative;
        

        var corpo = JsonConvert.SerializeObject(post);
        var Bytes = Encoding.UTF8.GetBytes(corpo);

        channel.BasicPublish(exchange: "Fluxo-Publicacao",
                    routingKey: "ServicoSalvaDB",
                    basicProperties: null,
                    body: Bytes);


    };


    channel.BasicConsume(queue: "ServicoSentimentos",
                            autoAck: true,
                            consumer: eventoProcessaSentimentoDoPost);

    Console.ReadLine();
}

async Task ConfiguraEmocaoEhSentimento(Post post)
{
        var url = $"https://apis.paralleldots.com/v4/sentiment";
        var client = new HttpClient();
        var response = await client.PostAsJsonAsync(url,new {
            Text = post.Conteudo,
            Api_key = "jCJb7NaDVlLx65qyHh9UrGe5p7hik7BkdldH5JaQbf4"
        });

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception { };
        }
        var resultadoDaApi = await response.Content.ReadFromJsonAsync<SentimentResultViewModel>();
        
    //


}