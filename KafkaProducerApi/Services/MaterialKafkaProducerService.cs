using Confluent.Kafka;
using KafkaPocCommon.DTOs;
using KafkaPocCommon.Infrastructure;
using KafkaProducerApi.Infrastructure;
using Microsoft.Extensions.Options;

namespace KafkaProducerApi.Services
{
    public class MaterialKafkaProducerService
    {
        private readonly ILogger<MaterialKafkaProducerService> _logger;
        private readonly IOptions<KafkaSettings> _kafkaSettings;
        private readonly IProducer<long, MaterialDto> _producer;
        public MaterialKafkaProducerService(ILogger<MaterialKafkaProducerService> logger, IOptions<KafkaSettings> kafkaSettings)
        {
            _logger = logger;
            _kafkaSettings = kafkaSettings;

            var config = new ProducerConfig
            {
                BootstrapServers = kafkaSettings.Value.BootstrapServers,
            };
            _producer = new ProducerBuilder<long, MaterialDto>(config)
                .SetKeySerializer(new KafkaSerializer<long>())
                .SetValueSerializer(new KafkaSerializer<MaterialDto>())
                .Build();
        }

        public async Task SendMessage(MaterialDto message, CancellationToken cancellationToken)
        {
            try
            {
                string topic = _kafkaSettings.Value.MaterialTopic;

                await _producer.ProduceAsync(topic, new Message<long, MaterialDto>
                {
                    Key = DateTime.Now.ToFileTimeUtc(),
                    Value = message
                },
                cancellationToken);

                _logger.LogInformation($"Material object was send to topic '{topic}'.");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error sending message to Kafka: {ex.Message}");
                throw;
            }
        }
    }
}
