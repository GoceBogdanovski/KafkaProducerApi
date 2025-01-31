using Confluent.Kafka;
using Newtonsoft.Json;
using System.Text;

namespace KafkaProducerApi.Infrastructure
{
    public class KafkaSerializer<T> : ISerializer<T>
    {
        public byte[] Serialize(T data, SerializationContext context)
        {
            if (data == null)
            {
                return null;
            }
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));
        }
    }
}
