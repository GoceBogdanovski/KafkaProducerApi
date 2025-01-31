using Confluent.Kafka;
using KafkaPocCommon.DTOs;
using KafkaPocCommon.Infrastructure;
using KafkaProducerApi.Infrastructure;
using Microsoft.Extensions.Options;

namespace KafkaProducerApi.Services
{
    public class ShapeKafkaProducerService
    {
        private readonly ILogger<ShapeKafkaProducerService> _logger;
        private readonly IOptions<KafkaSettings> _kafkaSettings;
        private readonly IProducer<long, ShapeDto> _producer;

        public ShapeKafkaProducerService(ILogger<ShapeKafkaProducerService> logger, IOptions<KafkaSettings> kafkaSettings)
        {
            _logger = logger;
            _kafkaSettings = kafkaSettings;

            var config = new ProducerConfig
            {
                BootstrapServers = kafkaSettings.Value.BootstrapServers
            };
            _producer = new ProducerBuilder<long, ShapeDto>(config)
                .SetKeySerializer(new KafkaSerializer<long>())
                .SetValueSerializer(new KafkaSerializer<ShapeDto>())
                .Build();
        }

        public async Task SendMessage(ShapeDto message, CancellationToken cancellationToken)
        {
            try
            {
                string topic = _kafkaSettings.Value.ShapeTopic;

                await _producer.ProduceAsync(topic, new Message<long, ShapeDto>
                {
                    Key = DateTime.Now.ToFileTimeUtc(),
                    Value = message
                },
                cancellationToken);

                _logger.LogInformation($"Shape oobject was send to topic '{topic}'.");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error sending message to Kafka: {ex.Message}");
                throw;
            }
        }
    }
}
