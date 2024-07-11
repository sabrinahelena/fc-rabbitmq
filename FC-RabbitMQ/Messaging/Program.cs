using FC_RabbitMQ.Configuration;
using Microsoft.Extensions.Configuration;

namespace FC_RabbitMQ.Messaging
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var exchangeSettings = config.GetSection("ExchangeSettings").Get<ExchangeSettings>();
            var rabbitMQConfig = config.GetSection("RabbitMQConfig").Get<RabbitMQConfig>();

            Console.WriteLine("Specify the action ('publish' or 'subscribe'):");
            string action = Console.ReadLine()?.Trim().ToLower();

            if (action != "publish" && action != "subscribe")
            {
                Console.WriteLine("Invalid action. Please specify 'publish' or 'subscribe'.");
                return;
            }

            string exchangeType = GetExchangeType(exchangeSettings);

            if (exchangeType == null)
            {
                Console.WriteLine("No valid exchange type found in configuration.");
                return;
            }

            rabbitMQConfig.RoutingKey = exchangeType == "topic" ? "sasa.key" : "sasa-routing-key";

            if (action == "publish")
            {
                await Publisher.PublishMessages(rabbitMQConfig, exchangeType);
            }
            else if (action == "subscribe")
            {
                await Subscriber.SubscribeToMessages(rabbitMQConfig, exchangeType);
            }
        }

        private static string GetExchangeType(ExchangeSettings settings)
        {
            if (settings.Direct) return "direct";
            if (settings.Topic) return "topic";
            if (settings.Fanout) return "fanout";
            return null;
        }
    }

    public class ExchangeSettings
    {
        public bool Direct { get; set; }
        public bool Topic { get; set; }
        public bool Fanout { get; set; }
    }
}
