using Newtonsoft.Json;

namespace CommonUtilities.Serializers
{
    public class JsonSerializer : ISerializer
    {
        public T Deserialize<T>(string item)
        {
            T result = JsonConvert.DeserializeObject<T>(item);
            return result;
        }

        public string Serialize<T>(T item)
        {
            string result = JsonConvert.SerializeObject(item);
            return result;
        }
    }
}
