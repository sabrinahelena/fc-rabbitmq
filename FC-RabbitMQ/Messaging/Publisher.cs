using FC_RabbitMQ.Configuration;
using RabbitMQ.Client;
using System.Text;

namespace FC_RabbitMQ.Messaging;

/// <summary>
/// Class responsible for publishing messages to RabbitMQ.
/// </summary>
public static class Publisher
{
    public static async Task PublishMessages(RabbitMQConfig config, string exchangeType)
    {
        var rabbitMQService = new RabbitMQService(config, exchangeType);
        using var connection = rabbitMQService.CreateConnection();
        using var channel = rabbitMQService.CreateChannel(connection);

        for (int i = 0; i < 5; i++)
        {
            string message = $"{i}";
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: config.ExchangeName, routingKey: config.RoutingKey, basicProperties: null, body: body);

            Console.WriteLine($"Mensagem {message} enviada com sucesso");
            await Task.Delay(1000);
        }
    }
}