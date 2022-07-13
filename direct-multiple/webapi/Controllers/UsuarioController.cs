using System.Text;
using Microsoft.AspNetCore.Mvc;
using Model;
using Newtonsoft.Json;

using RabbitMQ.Client;

namespace webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class UsuarioController : ControllerBase
{
    private ConnectionFactory _factory;
    public UsuarioController(){
        _factory = new ConnectionFactory()
                        {
                        HostName = "164.92.158.32",
                        UserName = "admin",
                        Password = "admin"
                        };

    }

    [HttpGet("PostUsuarioAdmin")]
    public string PostUsuarioAdmin([FromQuery] UsuarioAdmin user)
    {
       try{
            using (var connection = _factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                    var body = JsonConvert.SerializeObject(user);
                    var usuarioBytes = Encoding.UTF8.GetBytes(body);

                    channel.BasicPublish(exchange: "salvaUsuario",
                                    routingKey: "usuarioAdmin",
                                    basicProperties: null,
                                    body: usuarioBytes);
            }
            return "Salvo com sucesso";
        }catch(Exception e){
            return "Algo deu errado:" + e.Message;
        }
    }
    
    [HttpGet("PostUsuarioNormal")]
    public string PostUsuarioNormal([FromQuery] UsuarioNormal user)
    {
        try{
            using (var connection = _factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                    var body = JsonConvert.SerializeObject(user);
                    var usuarioBytes = Encoding.UTF8.GetBytes(body);

                    channel.BasicPublish(exchange: "salvaUsuario",
                                    routingKey: "usuarioNormal",
                                    basicProperties: null,
                                    body: usuarioBytes);
            }
            return "Salvo com sucesso";
        }catch(Exception e){
            return "Algo deu errado:" + e.Message;
        }
        
    }
}
