using System;

namespace Lib
{
    public class JsonSerializer : ISerializer
    {        
        public object Deserialize(string str)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject(str);
        }

        public T Deserialize<T>(string str)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(str);
        }

        public string Serialize(object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }
    }
}
