using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using FC_RabbitMQ.Configuration;

namespace FC_RabbitMQ;

/// <summary>
/// Class responsible for subscribing to messages from RabbitMQ.
/// </summary>
public static class Subscriber
{
    public static async Task SubscribeToMessages(RabbitMQConfig config, string exchangeType)
    {
        var rabbitMQService = new RabbitMQService(config, exchangeType);
        using var connection = rabbitMQService.CreateConnection();
        using var channel = rabbitMQService.CreateChannel(connection);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine($"Mensagem {message} recebida!");
        };

        channel.BasicConsume(queue: config.QueueName, autoAck: true, consumer: consumer);

        Console.WriteLine("Pressione [enter] para sair.");
        Console.ReadLine();
    }
}