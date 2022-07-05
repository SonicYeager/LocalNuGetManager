using LocalNuGetManager.Operations.Contracts.Operations;
using Newtonsoft.Json;

namespace LocalNuGetManager.Operations.Operations
{
    public class NewtonsoftJsonSerializer<TObject> : IJsonSerializer<TObject>
    {
        private readonly JsonSerializer _serializer;
        
        public NewtonsoftJsonSerializer()
        {
            _serializer = JsonSerializer.CreateDefault();
        }

        public TObject Deserialize(TextReader reader)
        {
            return (TObject)_serializer.Deserialize(reader, typeof(TObject));
        }
        public void Serialize(TextWriter writer, TObject obj)
        {
            _serializer.Serialize(writer, obj);
        }
    }
}
