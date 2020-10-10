using Newtonsoft.Json;

namespace DataHelper
{
    public static class Serialization
    {
        public static string SerializeMessage(MESSAGE value)
        {
            return JsonConvert.SerializeObject(value);
        }

        public static MESSAGE DeserializeMessage(string value)
        {
            return JsonConvert.DeserializeObject<MESSAGE>(value);
        }
    }
}