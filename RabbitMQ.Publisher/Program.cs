using RabbitMQ.Client;
using System;
using System.Linq;
using System.Text;

namespace RabbitMQ.Publisher
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://ibziucpv:JvP33KJ3sKOcsRpxRUmE1AAtbvjSIWtB@cougar.rmq.cloudamqp.com/ibziucpv");
            using var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.QueueDeclare("hello-gueue", true, false,false);//kuyruk yapısı
            Enumerable.Range(1, 50).ToList().ForEach(x =>
            {
                string message = $"Message{x}";
                var messageBody = Encoding.UTF8.GetBytes(message);//bit olarak gönderdik
                channel.BasicPublish(string.Empty, "hello-queue", null, messageBody);
                Console.WriteLine($"Mesaj gönderilmiştir: {message}");
            });          
            Console.ReadLine();
        }
    }
}
