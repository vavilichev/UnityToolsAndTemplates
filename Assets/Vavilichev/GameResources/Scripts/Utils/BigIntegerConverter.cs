using System;
using System.Numerics;
using Newtonsoft.Json;

namespace Vavilichev.GameResources.Utils
{
    public class BigIntegerConverter : JsonConverter<BigInteger>
    {
        public override void WriteJson(
            JsonWriter writer,
            BigInteger value,
            JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }

        public override BigInteger ReadJson(
            JsonReader reader,
            Type objectType,
            BigInteger existingValue,
            bool hasExistingValue,
            JsonSerializer serializer)
        {
            var value = (string)reader.Value;
            return BigInteger.Parse(value);
        }
    }
}