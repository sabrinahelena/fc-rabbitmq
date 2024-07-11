using RabbitMQ.Client;

namespace FC_RabbitMQ.Configuration;

public class RabbitMQService
{
    private readonly RabbitMQConfig _config;
    private readonly string _exchangeType;

    public RabbitMQService(RabbitMQConfig config, string exchangeType)
    {
        _config = config;
        _exchangeType = exchangeType;
    }

    public IConnection CreateConnection()
    {
        var factory = new ConnectionFactory() { HostName = _config.HostName, UserName = _config.UserName, Password = _config.Password };
        return factory.CreateConnection();
    }

    public IModel CreateChannel(IConnection connection)
    {
        var channel = connection.CreateModel();
        channel.ExchangeDeclare(exchange: _config.ExchangeName, type: _exchangeType);
        channel.QueueDeclare(queue: _config.QueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        channel.QueueBind(queue: _config.QueueName, exchange: _config.ExchangeName, routingKey: _config.RoutingKey);
        return channel;
    }
}