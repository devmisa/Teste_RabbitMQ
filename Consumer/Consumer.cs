using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Consumer
{
    class Consumer
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

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    Console.WriteLine($"[x] Recebida: {message}");
                };

                channel.BasicConsume(queue: "TesteRabbitMQ",
                                     autoAck: true,
                                     consumer: consumer);
                Console.ReadLine();
            }
        }
    }
}
