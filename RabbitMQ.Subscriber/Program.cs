using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace RabbitMQ.Subscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://ibziucpv:JvP33KJ3sKOcsRpxRUmE1AAtbvjSIWtB@cougar.rmq.cloudamqp.com/ibziucpv");
            using var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            //channel.QueueDeclare("hello-gueue", true, false, false);

            channel.BasicQos(0,1,false);
            var consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume("hello-queue", false,consumer);
            consumer.Received += (object sender, BasicDeliverEventArgs e) =>
              {
                  var message = Encoding.UTF8.GetString(e.Body.ToArray());
                  Console.WriteLine("Gelen Mesaj:" + message);
                  channel.BasicAck(e.DeliveryTag, false);
              };
            Console.ReadLine();
        }
    }
}
