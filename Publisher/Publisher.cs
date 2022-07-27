using RabbitMQ.Client;
using System;
using System.Text;

namespace Publisher
{
    class Publisher
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() // Definimos uma conexão com um nó RabbitMQ em localhost
            {
                HostName = "localhost"
            };

            using (var connection = factory.CreateConnection()) // Abrimos uma conexão com um nó RabbitMQ

            using (var channel = connection.CreateModel()) // Criamos um canal onde vamos definir uma fila, uma mensagem e publicar a mensagem
            {
                channel.QueueDeclare(queue: "TesteRabbitMQ",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string message = "Bem vindo ao RabbitMQ";

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "TesteRabbitMQ",
                                     basicProperties: null,
                                     body: body);

                Console.WriteLine($"[x] Enviada: {message}");

                Console.ReadLine();
            }
        }
    }
}
