using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace SticksCandy.Services.Save
{
    public class Vector3JsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Vector3);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null)
            {
                return default;
            }
            
            var jObject = JObject.Load(reader);  
            
            var vector = new Vector3
            (
                (float)jObject["x"],
                (float)jObject["y"],
                (float)jObject["z"]
            );
            
            return vector;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var vector = (Vector3)value;
            
            serializer.Serialize(writer, new { vector.x, vector.y, vector.z });
        }
    }
}