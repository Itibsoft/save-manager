using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SticksCandy.Services.Save
{
    public class JSONSerializer : ISerializer<string>
    {
        private JsonSerializerSettings _serializerSettings;
        private JsonSerializer _newtonsoftJsonSerializer;

        public JSONSerializer()
        {
            var converters = new JsonConverter[]
            {
                new ColorJsonConverter(),
                new Vector3JsonConverter()
            };
            
            _serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                Converters = converters
            };
            
            _newtonsoftJsonSerializer = JsonSerializer.Create(_serializerSettings);
        }
        
        public async Task<string> Serialize<TData>(TData data)
        {
            using var stream = new MemoryStream();
            
            await SerializeStream(data, stream).ConfigureAwait(false);

            var bytes = stream.ToArray();
            
            return Encoding.UTF8.GetString(bytes);
        }

        public async Task<TData> Deserialize<TData>(string data)
        {
            var buffer = Encoding.UTF8.GetBytes(data);
            
            var stream = new MemoryStream(buffer);
            
            return await DeserializeStream<TData>(stream);
        }

        private Task<TData> DeserializeStream<TData>(Stream stream)
        {
            return Task.Run(DeserializeProcess);

            TData DeserializeProcess()
            {
                using (stream)
                using (var streamReader = new StreamReader(stream))
                using (var jsonReader = new JsonTextReader(streamReader))
                {
                    return _newtonsoftJsonSerializer.Deserialize<TData>(jsonReader);
                }
            }
        }
        
        private Task SerializeStream<TData>(TData data, Stream stream)
        {
            return Task.Run(SerializeProcess);
            
            void SerializeProcess()
            {
                using var streamWriter = new StreamWriter(stream);
                
                _newtonsoftJsonSerializer.Serialize(streamWriter, data);
            }
        }
    }
}