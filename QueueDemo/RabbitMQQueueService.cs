using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QueueDemo
{
    public class RabbitMQQueueService : IQueueService
    {
        private readonly IConnection connection;
        private readonly IChannel channel;
        private readonly string queueName = "myQueue2";

        public RabbitMQQueueService(string hostName, string userName, string password)
        {
            try
            {
                Console.WriteLine("Configurando conexión a RabbitMQ...");

                
                var factory = new ConnectionFactory()
                {
                    HostName = hostName,
                    UserName = userName,
                    Password = password,
                    VirtualHost = "/",
                    Port = 5672,
                    RequestedHeartbeat = TimeSpan.FromSeconds(60)
                };

                Console.WriteLine("Conectando a RabbitMQ...");

                
                connection = factory.CreateConnectionAsync().GetAwaiter().GetResult();

                Console.WriteLine($"Conexión establecida. Nombre del servidor: {connection.Endpoint.HostName}");

                channel = connection.CreateChannelAsync().GetAwaiter().GetResult();

                Console.WriteLine("Canal creado. Declarando cola...");

                
                var queueResult = channel.QueueDeclareAsync(
                    queue: queueName,
                    durable: false,  
                    exclusive: false,
                    autoDelete: false,
                       arguments: null)
    .GetAwaiter().GetResult();

                Console.WriteLine($"Cola '{queueName}' declarada exitosamente. Mensaje count: {queueResult.MessageCount}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR CRÍTICO al inicializar RabbitMQ: {ex.GetType().Name}: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                throw;
            }
        }

        public async Task EnqueueAsync(string message)
        {
            try
            {
                var body = Encoding.UTF8.GetBytes(message);

                Console.WriteLine($"Intentando publicar mensaje: {message}");

                
                await channel.BasicPublishAsync(
                    exchange: "",
                    routingKey: queueName,
                    body: body);

                Console.WriteLine($"[Enqueue] Mensaje publicado: {message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al publicar mensaje: {ex.GetType().Name}: {ex.Message}");
                throw;
            }
        }

        public async Task<string> DequeueAsync()
        {
            try
            {
                Console.WriteLine("Intentando obtener mensaje de la cola...");

                var result = await channel.BasicGetAsync(
                    queue: queueName,
                    autoAck: true,
                    cancellationToken: default);

                if (result != null)
                {
                    var message = Encoding.UTF8.GetString(result.Body.ToArray());
                    Console.WriteLine($"[Dequeue] Mensaje recibido: {message}");
                    return message;
                }
                else
                {
                    Console.WriteLine("No hay mensajes en la cola.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener mensaje: {ex.GetType().Name}: {ex.Message}");
                throw;
            }
        }

        public void Dispose()
        {
            try
            {
                Console.WriteLine("Cerrando conexiones...");
                channel?.Dispose();
                connection?.Dispose();
                Console.WriteLine("Conexiones cerradas correctamente");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cerrar conexiones: {ex.Message}");
            }
        }
    }
}

