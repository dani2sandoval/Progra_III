using System;
using System.Threading.Tasks;

namespace QueueDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                string hostName = "localhost";
                string userName = "guest";
                string password = "guest";

                Console.WriteLine("=== INICIANDO DEMO DE RABBITMQ ===");
                Console.WriteLine($"Conectando a: {hostName}");

                using (IQueueService queueService = new RabbitMQQueueService(hostName, userName, password))
                {
                    
                    for (int i = 1; i <= 5; i++)
                    {
                        var message = $"Mensaje de prueba #{i} - {DateTime.Now}";
                        Console.WriteLine($"Enviando: {message}");
                        await queueService.EnqueueAsync(message);
                        
                        await Task.Delay(500);
                    }

                    Console.WriteLine("\nMensajes enviados. Presiona ENTER para consumirlos...");
                    Console.ReadLine();

                    
                    for (int i = 1; i <= 5; i++)
                    {
                        string mensaje = await queueService.DequeueAsync();
                        if (mensaje != null)
                        {
                            Console.WriteLine($"Recibido #{i}: {mensaje}");
                        }
                        else
                        {
                            Console.WriteLine($"No se encontraron más mensajes después de {i - 1} intentos.");
                            break;
                        }
                        
                        await Task.Delay(500);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR FATAL: {ex.GetType().Name}: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }

            Console.WriteLine("\n=== DEMO FINALIZADA ===");
            Console.WriteLine("Presiona cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}
