
namespace HttpService
{
    public interface ISerializationHelper
    {
        public string Serialize(object obj);
        public T? Deserialize<T>(string content);
    }
}
